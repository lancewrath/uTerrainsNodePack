using LibNoise;
using LibNoise.Primitive;
using UltimateTerrains;
using UltimateTerrains.Utils;
using System;

public class VoronoiPrimitive : Primitive2DNode
{

    private readonly double displacement = 1.0;
    private readonly double frequency = 1.0;
    private readonly int seed;
    private readonly bool distance;

    private readonly NoiseQuality quality;

    public VoronoiPrimitive(double frequency, bool distance, double displacement, int seed, NoiseQuality quality)
    {
        this.frequency = frequency;
        this.displacement = displacement;
        this.distance = distance;
        this.quality = quality;
        //perlin = new ImprovedPerlin(seed, quality);
    }

    public override double Execute(double x, double y, double z, CallableNode flow)
    {
        x *= frequency;
        y *= frequency;
        z *= frequency;
        var xi = (x > 0.0 ? (int)x : (int)x - 1);
        var iy = (y > 0.0 ? (int)y : (int)y - 1);
        var iz = (z > 0.0 ? (int)z : (int)z - 1);
        var md = 2147483647.0;
        double xc = 0;
        double yc = 0;
        double zc = 0;
        for (var zcu = iz - 2; zcu <= iz + 2; zcu++)
        {
            //for (var ycu = iy - 2; ycu <= iy + 2; ycu++)
            //{
                for (var xcu = xi - 2; xcu <= xi + 2; xcu++)
                {
                    var xp = xcu + uUtils.ValueNoise3D(xcu, 0, zcu, seed);
                    //var yp = ycu + uUtils.ValueNoise3D(xcu, ycu, zcu, seed + 1);
                    var zp = zcu + uUtils.ValueNoise3D(xcu, 0, zcu, seed + 2);
                    var xd = xp - x;
                    //var yd = yp - y;
                    var zd = zp - z;
                    var d = xd * xd + zd * zd;
                    //var d = xd * xd + yd * yd + zd * zd;
                    if (d < md)
                    {
                        md = d;
                        xc = xp;
                        //yc = yp;
                        zc = zp;
                    }
                }
           // }
        }
        double v;
        if (distance)
        {
            var xd = xc - x;
            //var yd = yc - y;
            var zd = zc - z;
            v = (Math.Sqrt(xd * xd + zd * zd)) * uUtils.Sqrt3 - 1.0;
            //v = (Math.Sqrt(xd * xd + yd * yd + zd * zd)) * uUtils.Sqrt3 - 1.0;
        }
        else
        {
            v = 0.0;
        }
        return (v + (displacement * uUtils.ValueNoise3D((int)(Math.Floor(xc)), 0,
            (int)(Math.Floor(zc)), seed)));
    }


}
