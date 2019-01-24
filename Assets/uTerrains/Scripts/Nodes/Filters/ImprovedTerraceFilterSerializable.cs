using System;
using System.Collections.Generic;
using UltimateTerrains;
using UltimateTerrainsEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
[PrettyTypeName("Improved Terrace")]
[Serializable]
public class ImprovedTerraceFilterSerializable : FilterNodeSerializable
{

    public override string Title
    {
        get { return "Improved Terrace"; }
    }

    [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(-1, -1, 1, 1);
    [SerializeField] private int steps;
    [SerializeField] private bool useCurve = false;
    [SerializeField] private bool inverted = false;
    bool collapsed = true;

    public override void OnEditorGUI(UltimateTerrain uTerrain)
    {
#if UNITY_EDITOR
        inverted = EditorGUILayout.Toggle("Inverted", inverted);
        useCurve = EditorGUILayout.Toggle("Use Curve", useCurve);
        if (useCurve)
        {
            curve = EditorGUILayout.CurveField(curve, GUILayout.Width(100), GUILayout.Height(80));
        }
        else
        {
            steps = EditorGUILayout.IntField("Steps", steps);
        }
        base.OnEditorGUI(uTerrain);
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new ImprovedTerraceFilter(inputs[0], inputs[1], Intensity, curve, steps, inverted, useCurve);
    }
}
