using System;
using System.Collections.Generic;
using UltimateTerrains;
using UltimateTerrainsEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
[PrettyTypeName("Turbulence")]
[Serializable]
public class TurbulenceFilterSerializable : FilterNodeSerializable
{
    public override string Title
    {
        get { return "Turbulence"; }
    }



    [SerializeField] private float frequency;
    [SerializeField] private float power;
    [SerializeField] private int seed;

    public override void OnEditorGUI(UltimateTerrain uTerrain)
    {
#if UNITY_EDITOR
        EditorGUIUtility.labelWidth = 60;
        frequency = EditorGUILayout.FloatField("Frequency", frequency);
        power = EditorGUILayout.FloatField("Power", power);
        seed = EditorGUILayout.IntField("Seed", seed);
        base.OnEditorGUI(uTerrain);
        EditorGUIUtility.labelWidth = 0;
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new TurbulenceFilter(inputs[0], inputs[1], Intensity, frequency, power, seed);
    }
}
