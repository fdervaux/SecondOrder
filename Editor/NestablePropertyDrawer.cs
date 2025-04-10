using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Package.SecondOrder.Editor
{
	/// <summary>
	/// NestablePropertyDrawer is a custom property drawer that allows for nested properties.
	/// </summary>
	public class NestablePropertyDrawer : PropertyDrawer
	{
		private bool _initialized;
		protected object PropertyObject;
		private Type _objectType;
	
		private static readonly Regex MatchArrayElement = new Regex(@"^data\[(\d+)\]$");
	
		/// <summary>
		/// Initializes the property drawer with the given SerializedProperty.
		/// </summary>
		/// <param name="prop"></param>
		/// <returns></returns>
		protected virtual void Initialize(SerializedProperty prop)
		{
			if (_initialized)
				return;

			SerializedObject serializedObject = prop.serializedObject;
			string path = prop.propertyPath;
	
			PropertyObject = serializedObject == null || serializedObject.targetObject == null ? null : serializedObject.targetObject;
			_objectType = PropertyObject?.GetType();
			if (!string.IsNullOrEmpty(path) && PropertyObject != null)
			{
				string[] splitPath = path.Split('.');
				Type fieldType = null;

				//work through the given property path, node by node
				for (int i = 0; i < splitPath.Length; i++)
				{
					string pathNode = splitPath[i];

					//both arrays and lists implement the IList interface
					if (fieldType != null && typeof(IList).IsAssignableFrom(fieldType))
					{
						//IList items are serialized like this: `Array.data[0]`
						Debug.AssertFormat(pathNode.Equals("Array", StringComparison.Ordinal), serializedObject?.targetObject, "Expected path node 'Array', but found '{0}'", pathNode);

						//just skip the `Array` part of the path
						pathNode = splitPath[++i];

						//match the `data[0]` part of the path and extract the IList item index
						Match elementMatch = MatchArrayElement.Match(pathNode);
						
						if (elementMatch.Success && int.TryParse(elementMatch.Groups[1].Value, out int index))
						{
							IList objectArray = (IList)PropertyObject;
							bool validArrayEntry = objectArray != null && index < objectArray.Count;
							PropertyObject = validArrayEntry ? objectArray[index] : null;
							_objectType = fieldType.IsArray
								? fieldType.GetElementType()          //only set for arrays
								: fieldType.GenericTypeArguments[0];  //type of `T` in List<T>
						}
						else
						{
							Debug.LogErrorFormat(serializedObject?.targetObject, "Unexpected path format for array item: '{0}'", pathNode);
						}
						//reset fieldType, so we don't end up in the IList branch again next iteration
						fieldType = null;
					}
					else
					{
						FieldInfo field;
						Type instanceType = _objectType;
						BindingFlags fieldBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
						do
						{
							field = instanceType?.GetField(pathNode, fieldBindingFlags);

							//b/c a private, serialized field of a subclass isn't directly retrievable,
							fieldBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
							//if necessary, work up the inheritance chain until we find it.
							instanceType = instanceType?.BaseType;
						}
						while (field == null && instanceType != typeof(object));

						//store object info for next iteration or to return
						PropertyObject = field == null || PropertyObject == null ? null : field.GetValue(PropertyObject);
						fieldType = field == null ? null : field.FieldType;
						_objectType = fieldType;
					}
				}
			}
			_initialized = true;
		}
	
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			Initialize(prop);
		}
	}
}
