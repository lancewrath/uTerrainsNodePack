using System;
using System.Collections.Generic;
using LibNoise;
using UltimateTerrains;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[PrettyTypeName("Billow 2D")]
[Serializable]
public class BillowPrimitiveSerializable : Primitive2DNodeSerializable
{

    public override string Title
    {
        get { return "Billow 2D"; }
    }

    // Useful properties for the module
    [SerializeField] private float frequency = 1f / 90f;
    [SerializeField] private float lacunarity = 2.0f;
    [SerializeField] private int octaveCount = 6;
    [SerializeField] private int seed;
    [SerializeField] private float persistence = 2.0f;
    [SerializeField] private NoiseQuality quality = NoiseQuality.Standard;

    public override void OnEditorGUI(UltimateTerrain uTerrain)
    {
#if UNITY_EDITOR
        frequency = EditorGUILayout.FloatField("Frequency:", frequency);
        lacunarity = EditorGUILayout.FloatField("Lacunarity:", lacunarity);
        persistence = EditorGUILayout.FloatField("Persistance:", persistence);
        octaveCount = EditorGUILayout.IntField("Octaves:", octaveCount);
        seed = EditorGUILayout.IntField("Seed:", seed);
        quality = (NoiseQuality)EditorGUILayout.EnumPopup("Quality:", quality);
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new BillowPrimitive(frequency, lacunarity, seed, octaveCount, persistence, quality);
    }
}
