using UltimateTerrains;
using UnityEngine;
using UltimateTerrains.Utils;
public class ImprovedTerraceFilter : FilterNode
{
    private readonly AnimationCurve curve;
    private readonly int steps;
    private readonly bool useCurve;
    private readonly bool inverted;

    public ImprovedTerraceFilter(CallableNode input, CallableNode maskInput, double intensity, AnimationCurve curve, int steps, bool inverted, bool useCurve) : base(input, maskInput, intensity)
    {
        // Create a copy of the animation curve to avoid any concurrency issue
        this.curve = new AnimationCurve(curve.keys);
        this.steps = steps;
        this.useCurve = useCurve;
    }


    protected override double ExecuteFilter(double x, double y, double z, CallableNode flow, double inputValue)
    {
        var smv = inputValue;
        int ip;
        double[] _data;
        if(useCurve)
        {
            _data = new double[curve.length];
            for (int i = 0; i < curve.length; i++)
            {
                _data[i] = curve[i].value;
            }
        } else
        {
            _data = new double[steps];
            for (int i = 0; i < steps; i++)
            {
                _data[i] = -1.0 + ((2.0 / steps) * (i+1));
            }
            
        }
        for (ip = 0; ip < _data.Length; ip++)
        {
            if (smv < _data[ip])
            {
                break;
            }
        }
        var i0 = Mathf.Clamp(ip - 1, 0, _data.Length - 1);
        var i1 = Mathf.Clamp(ip, 0, _data.Length - 1);
        if (i0 == i1)
        {
            return _data[i1];
        }
        var v0 = _data[i0];
        var v1 = _data[i1];
        var a = (smv - v0) / (v1 - v0);
        if (inverted)
        {
            a = 1.0 - a;
            var t = v0;
            v0 = v1;
            v1 = t;
        }
        a *= a;
        return uUtils.InterpolateLinear(v0, v1, a);
    }

}
