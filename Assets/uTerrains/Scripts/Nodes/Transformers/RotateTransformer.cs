using System;
using UnityEngine;
using UltimateTerrains;

public class RotateTransformer : TransformerNode
{
    public override bool Is2D
    {
        get { return true; }
    }

    private readonly double _x;
    private readonly double _x1Matrix;
    private readonly double _x2Matrix;
    private readonly double _x3Matrix;
    private readonly double _y;
    private readonly double _y1Matrix;
    private readonly double _y2Matrix;
    private readonly double _y3Matrix;
    private readonly double _z;
    private readonly double _z1Matrix;
    private readonly double _z2Matrix;
    private readonly double _z3Matrix;

    // Use this for initialization
    public RotateTransformer(CallableNode input, double x, double y, double z) : base(input)
    {
        var xc = Math.Cos(x * Mathf.Deg2Rad);
        var yc = Math.Cos(y * Mathf.Deg2Rad);
        var zc = Math.Cos(z * Mathf.Deg2Rad);
        var xs = Math.Sin(x * Mathf.Deg2Rad);
        var ys = Math.Sin(y * Mathf.Deg2Rad);
        var zs = Math.Sin(z * Mathf.Deg2Rad);
        _x1Matrix = ys * xs * zs + yc * zc;
        _y1Matrix = xc * zs;
        _z1Matrix = ys * zc - yc * xs * zs;
        _x2Matrix = ys * xs * zc - yc * zs;
        _y2Matrix = xc * zc;
        _z2Matrix = -yc * xs * zc - ys * zs;
        _x3Matrix = -ys * xc;
        _y3Matrix = xs;
        _z3Matrix = yc * xc;
        _x = x;
        _y = y;
        _z = z;
    }

    public override double Execute(double x, double y, double z, CallableNode flow)
    {

        var nx = (_x1Matrix * x) + (_y1Matrix * y) + (_z1Matrix * z);
        var ny = (_x2Matrix * x) + (_y2Matrix * y) + (_z2Matrix * z);
        var nz = (_x3Matrix * x) + (_y3Matrix * y) + (_z3Matrix * z);
        return flow.Call(Input,nx, ny, nz);

    }
}
