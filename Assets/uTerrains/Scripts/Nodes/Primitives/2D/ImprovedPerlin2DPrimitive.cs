using UltimateTerrains;
using UltimateTerrains.Utils;
using LibNoise;
public class ImprovedPerlin2DPrimitive : Primitive2DNode
{

    private readonly double frequency = 1.0;
    private readonly double lacunarity = 2.0;
    private readonly NoiseQuality quality = NoiseQuality.Standard;
    private readonly int octaveCount = 6;
    private readonly double persistence = 0.5;
    private readonly int seed;
    private readonly double scale;

    public ImprovedPerlin2DPrimitive(double frequency, double scale, int seed, double lacunarity, int octaveCount, double persistence, NoiseQuality quality)
    {
        this.frequency = frequency;
        this.lacunarity = lacunarity;
        this.octaveCount = octaveCount;
        this.quality = quality;
        this.persistence = persistence;
        this.seed = seed;
        this.scale = scale;
        
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
            value += signal * amplitude;
            x *= lacunarity;
            y *= lacunarity;
            z *= lacunarity;
            amplitude *= persistence;
        }
        return value;// *scale;
    }



}
