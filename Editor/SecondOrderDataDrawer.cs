using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SecondOrderData))]
public class SecondOrderDataDrawer : NestablePropertyDrawer
{
    private int graphSize = 300;
    protected SecondOrderData target { get { return (SecondOrderData)base.propertyObject; } }
    private SecondOrder<float> _secondOrder;
    private AnimationCurve _curve;
    private AnimationCurve _targetCurve;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + 2) * 4 + graphSize;
    }


    public void updateGraph(SerializedProperty property)
    {
        _secondOrder = new SecondOrder<float>();

        _secondOrder.Data = target;

        int keyCount = 1000;
        float step = (float)1 / keyCount;
        float value = 0;

        Keyframe[] keyFrames = new Keyframe[keyCount];

        for (int i = 0; i < keyCount; i++)
        {
            float target = i >= keyCount * 0.1f ? 1f : 0f;
            value = SecondOrderDynamics.SencondOrderUpdate(target, _secondOrder, step);
            keyFrames[i] = new Keyframe((float)i / keyCount, value);
        }

        _curve = new AnimationCurve(keyFrames);

        Keyframe[] keyFramesTarget = new Keyframe[keyCount];
        keyFramesTarget[0] = new Keyframe(0, 0);
        keyFramesTarget[1] = new Keyframe(0.1f, 0f);
        keyFramesTarget[2] = new Keyframe(0.101f, 1f);
        keyFramesTarget[3] = new Keyframe(1f, 1f);

        _targetCurve = new AnimationCurve(keyFramesTarget);
    }

    public void plotGraph(Rect graphRect)
    {
        EditorGUIUtility.DrawCurveSwatch(graphRect, _curve, null, Color.green, new Color(0, 0, 0, 0.2f), new Rect(0, -1f, 1f, 3f));
        EditorGUIUtility.DrawCurveSwatch(graphRect, _targetCurve, null, Color.red, new Color(0, 0, 0, 0.2f), new Rect(0, -1f, 1f, 3f));
    }

    protected override void Initialize(SerializedProperty prop)
	{
		base.Initialize(prop);
        //target.UpdateData();
		updateGraph(prop);
	}


    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position,property,label);

        EditorGUI.BeginProperty(position, label, property);

        // Calculate rects
        var frequencyRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        var dampingRect = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);
        var impulseRect = new Rect(position.x, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.BeginChangeCheck();
        
        EditorGUI.PropertyField(frequencyRect, property.FindPropertyRelative("frequency"), new GUIContent("frequency"));
        EditorGUI.PropertyField(dampingRect, property.FindPropertyRelative("damping"), new GUIContent("damping"));
        EditorGUI.PropertyField(impulseRect, property.FindPropertyRelative("impulse"), new GUIContent("impulse"));


        if (EditorGUI.EndChangeCheck())
        {
            updateGraph(property);
        }

        var graphRect = new Rect(position.x, position.y + 3 * (EditorGUIUtility.singleLineHeight + 2), position.width, graphSize);
        plotGraph(graphRect);

        EditorGUI.EndProperty();
    }
}