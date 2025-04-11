using Package.SecondOrder.Runtime;
using UnityEditor;
using UnityEngine;

namespace Package.SecondOrder.Editor
{
    /// <summary>
    /// Custom property drawer for the SecondOrderData class.
    /// </summary>
    [CustomPropertyDrawer(typeof(SecondOrderData))]
    public class SecondOrderDataDrawer : PropertyDrawer
    {
        private readonly int _graphSize = 300;
        private SecondOrderData _target;
        private SecondOrder<float> _secondOrder;
        private AnimationCurve _curve;
        private AnimationCurve _targetCurve;

        private bool _needUpdateGraph = true;
        private bool _initialized;

        /// <summary>
        /// Gets the height of the property drawer.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + 2) * 4 + _graphSize;
        }


        /// <summary>
        /// Updates the graph based on the current target data.
        /// </summary>
        private void UpdateGraph()
        {
            if (!_needUpdateGraph)
                return;

            _secondOrder = new SecondOrder<float>
            {
                Data = _target
            };

            const int keyCount = 1000;
            const float step = (float)1 / keyCount;

            Keyframe[] keyFrames = new Keyframe[keyCount];

            for (int i = 0; i < keyCount; i++)
            {
                float target = i >= keyCount * 0.1f ? 1f : 0f;
                float value = SecondOrderDynamics.Update(target, _secondOrder, step);
                keyFrames[i] = new Keyframe((float)i / keyCount, value);
            }

            _curve = new AnimationCurve(keyFrames);

            Keyframe[] keyFramesTarget = new Keyframe[keyCount];
            keyFramesTarget[0] = new Keyframe(0, 0);
            keyFramesTarget[1] = new Keyframe(0.1f, 0f);
            keyFramesTarget[2] = new Keyframe(0.101f, 1f);
            keyFramesTarget[3] = new Keyframe(1f, 1f);

            _targetCurve = new AnimationCurve(keyFramesTarget);

            _needUpdateGraph = false;
        }


        /// <summary>
        /// Plots the graph using the given rect.
        /// </summary>
        /// <param name="graphRect"></param>
        private void PlotGraph(Rect graphRect)
        {
            EditorGUIUtility.DrawCurveSwatch(graphRect, _curve, null, Color.green, new Color(0, 0, 0, 0.2f),
                new Rect(0, -1f, 1f, 3f));
            EditorGUIUtility.DrawCurveSwatch(graphRect, _targetCurve, null, Color.red, new Color(0, 0, 0, 0.2f),
                new Rect(0, -1f, 1f, 3f));
        }


        /// <summary>
        /// Initializes the property drawer.
        /// </summary>
        /// <param name="property"></param>
        private void Init(SerializedProperty property)
        {
            if (_initialized)
                return;

            _target = (SecondOrderData)DrawerHelper.GetTargetObjectOfProperty(property);

            _needUpdateGraph = true;
            _initialized = true;
        }

        /// <summary>
        /// OnGUI method for the custom property drawer.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);
            UpdateGraph();

            EditorGUI.BeginProperty(position, label, property);

            // Calculate rects
            Rect frequencyRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            Rect dampingRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2),
                position.width,
                EditorGUIUtility.singleLineHeight);
            Rect impulseRect = new Rect(position.x, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2),
                position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(frequencyRect, property.FindPropertyRelative("_frequency"),
                new GUIContent("frequency"));
            EditorGUI.PropertyField(dampingRect, property.FindPropertyRelative("_damping"), new GUIContent("damping"));
            EditorGUI.PropertyField(impulseRect, property.FindPropertyRelative("_impulse"), new GUIContent("impulse"));


            if (EditorGUI.EndChangeCheck())
            {
                _needUpdateGraph = true;
            }

            Rect graphRect = new(position.x, position.y + 3 * (EditorGUIUtility.singleLineHeight + 2), position.width,
                _graphSize);
            PlotGraph(graphRect);

            EditorGUI.EndProperty();
        }
    }
}