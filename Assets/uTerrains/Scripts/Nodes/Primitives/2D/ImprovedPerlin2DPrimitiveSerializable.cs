using System;
using System.Collections.Generic;
using LibNoise;
using UltimateTerrains;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[PrettyTypeName("Improved Perlin 2D")]
[Serializable]
public class ImprovedPerlin2DPrimitiveSerializable : Primitive2DNodeSerializable
{

    public override string Title
    {
        get { return "Improved Perlin 2D"; }
    }

    // Useful properties for the module
    [SerializeField] private float frequency = 1f / 90f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private int seed;
    [SerializeField] private NoiseQuality quality = NoiseQuality.Standard;
    [SerializeField] private float lacunarity = 2.0f;
    [SerializeField] private int octaveCount = 6;
    [SerializeField] private float persistence = 0.5f;


    public override void OnEditorGUI(UltimateTerrain uTerrain)
    {
#if UNITY_EDITOR
        frequency = EditorGUILayout.FloatField("Frequency:", frequency);
        scale = EditorGUILayout.FloatField("Scale:", scale);
        lacunarity = EditorGUILayout.FloatField("Lacunarity:", lacunarity);
        octaveCount = EditorGUILayout.IntSlider("Octaves", octaveCount, 1, 6);
        seed = EditorGUILayout.IntField("Seed:", seed);
        persistence = EditorGUILayout.FloatField("Persistence:", persistence);
        quality = (NoiseQuality)EditorGUILayout.EnumPopup("Quality:", quality);
#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new ImprovedPerlin2DPrimitive(frequency, scale, seed, lacunarity, octaveCount, persistence, quality);
    }


}
