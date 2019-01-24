using System;
using System.Collections.Generic;
using LibNoise;
using UltimateTerrains;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[PrettyTypeName("Voronoi 2D")]
[Serializable]
public class VoronoiPrimitiveSerializable : Primitive2DNodeSerializable
{
    public override string Title
    {
        get { return "Voronoi 2D"; }
    }

    [SerializeField] private float displacement = 0.5f;
    [SerializeField] private float frequency = 0.008f;
    [SerializeField] private int seed;
    [SerializeField] private bool distance = true;
   
    [SerializeField] private NoiseQuality quality;
    // Use this for initialization
    public override void OnEditorGUI(UltimateTerrain uTerrain)
    {
#if UNITY_EDITOR
        frequency = EditorGUILayout.FloatField("Frequency:", frequency);
        displacement = EditorGUILayout.FloatField("Displacement:", displacement);
        
        seed = EditorGUILayout.IntField("Seed:", seed);
        distance = EditorGUILayout.Toggle("Use Distance", distance);
        quality = (NoiseQuality)EditorGUILayout.EnumPopup("Quality:", quality);
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new VoronoiPrimitive(frequency,distance,displacement,seed,quality);
    }
}
