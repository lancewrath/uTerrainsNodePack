using LibNoise;
using LibNoise.Primitive;
using UltimateTerrains;
using UltimateTerrains.Utils;

public class BillowPrimitive : Primitive2DNode
{
    private readonly double frequency = 1.0;
    private readonly double lacunarity = 2.0;
    private readonly NoiseQuality quality = NoiseQuality.Standard;
    private readonly int octaveCount = 6;
    private readonly double persistence = 0.5;
    private readonly int seed;

    public BillowPrimitive(double frequency, double lacunarity, int seed, int octaveCount, double persistence, NoiseQuality quality)
    {
        this.frequency = frequency;
        this.lacunarity = lacunarity;
        this.octaveCount = octaveCount;
        this.persistence = persistence;
        this.quality = quality;
        //perlin = new ImprovedPerlin(seed, quality);
    }

    public override double Execute(double x, double y, double z, CallableNode flow)
    {

        var value = 0.0;
        var amplitude = 1.0;

        x *= frequency;
        y *= frequency;
        z *= frequency;
        for (var i = 0; i < octaveCount; i++)
        {
            var nx = uUtils.MakeInt32Range(x);
            var ny = uUtils.MakeInt32Range(y);
            var nz = uUtils.MakeInt32Range(z);
            var seed = (this.seed + i) & 0xffffffff;
            var signal = uUtils.GradientCoherentNoise3D(nx, 0, nz, seed, quality);
            signal = 2.0 * System.Math.Abs(signal) - 1.0;
            value += signal * amplitude;
            x *= lacunarity;
            y *= lacunarity;
            z *= lacunarity;
            amplitude *= persistence;
        }
        return value + 0.5;



    }

    


}
