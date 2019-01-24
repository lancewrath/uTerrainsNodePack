using UltimateTerrains;
using LibNoise;
using LibNoise.Primitive;

public class TurbulenceFilter : FilterNode
{


    private readonly double frequency;
    private readonly double power;
    private readonly int seed;
    private readonly ImprovedPerlin distort;
    private readonly CallableNode input;

    private const double X0 = (12414.0 / 65536.0);
    private const double Y0 = (65124.0 / 65536.0);
    private const double Z0 = (31337.0 / 65536.0);
    private const double X1 = (26519.0 / 65536.0);
    private const double Y1 = (18128.0 / 65536.0);
    private const double Z1 = (60493.0 / 65536.0);
    private const double X2 = (53820.0 / 65536.0);
    private const double Y2 = (11213.0 / 65536.0);
    private const double Z2 = (44845.0 / 65536.0);

    public TurbulenceFilter(CallableNode input, CallableNode maskInput, double intensity, double frequency, double power, int seed) : base(input, maskInput, intensity)
    {
        this.input = input;
        this.frequency = frequency;
        this.power = power;
        this.seed = seed;
        this.distort = new ImprovedPerlin(this.seed, NoiseQuality.Standard);
    }



    protected override double ExecuteFilter(double x, double y, double z, CallableNode flow, double inputValue)
    {

        var xd = x + (distort.GetValue((x*frequency) + X0, 0, (z * frequency) + Z0) * power);
        //var yd = y + (distort.GetValue((x * frequency) + X1, (y * frequency) + Y1, (z * frequency) + Z1) * power);
        var zd = z + (distort.GetValue((x * frequency) + X2, 0, (z * frequency) + Z2) * power);
        return flow.Call(input, xd, 0, zd);

    }


}
