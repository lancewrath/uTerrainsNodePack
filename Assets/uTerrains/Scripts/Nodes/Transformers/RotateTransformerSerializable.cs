using System;
using System.Collections.Generic;
using UltimateTerrains;
using UltimateTerrainsEditor;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif


[PrettyTypeName("Rotate")]
[Serializable]
public class RotateTransformerSerializable : TransformerNodeSerializable
{
    [SerializeField] private float _x = 0f;
    [SerializeField] private float _y = 0f;
    [SerializeField] private float _z = 0f;
    public override string Title
    {
        get { return "Rotate"; }
    }

    public override NodeLayer Layer
    {
        get { return NodeLayer.Layer2D; }
    }
    public override void OnEditorGUI(UltimateTerrain uTerrain)
    {
#if UNITY_EDITOR
        _x = EditorGUILayout.FloatField("Angle X:", _x);
        _y = EditorGUILayout.FloatField("Angle Y:", _y);
        _z = EditorGUILayout.FloatField("Angle Z:", _z);

#endif
    }

    public override IGeneratorNode CreateModule(UltimateTerrain uTerrain, List<CallableNode> inputs)
    {
        return new RotateTransformer(inputs[0], _x, _y, _z);
    }
}
