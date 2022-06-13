using System;
using System.Drawing;

namespace VisualLogic.Elements;

using Utils;

public class VisualSurface : VisualElement
{
    private Plot3DCamera camera;
    private double[] data;
    private int lenx;
    private int lenz;
    private double minx;
    private double minz;
    private double maxx;
    private double maxz;
    private double resolution;

    private double get(double x, double z)
    {
        if (x < minx || x > maxx || z < minz || z > maxz)
            return double.NaN;
        int i = (int)((x - minx) / resolution),
            k = (int)((z - minz) / resolution);
        return data[i + k * lenx];
    }

    private void set(double x, double z, double value)
    {
        if (x < minx || x > maxx || z < minz || z > maxz)
            return;
        int i = (int)((x - minx) / resolution),
            k = (int)((z - minz) / resolution);
        data[i + k * lenx] = value;
    }

    public VisualSurface(
        double minx, double maxx, 
        double minz, double maxz,
        double resolution,
        double ymaxx, double ymaxz)
    {
        this.lenx = (int)((maxx - minx) / resolution);
        this.lenz = (int)((maxz - minz) / resolution);
        this.data = new double[this.lenx * this.lenz];
        this.minx = minx;
        this.minz = minz;
        this.maxx = maxx;
        this.minx = minx;
        this.resolution = resolution;

        double cx = (maxx + minx) / 2;
        double cz = maxz + 1.0;
        double cy = 5.0;

        Random rand =new Random(DateTime.Now.Millisecond);
        camera = new Plot3DCamera((cx, cy, cz),
            Vector.uZ, 1.0, Vector.uX, Vector.uY);
    }

    public double this[double x, double z]
    {
        get
        {
            return get(x, z);
        }
        set
        {
            set(x, z, value);
            Update();
        }
    }
    protected override void Draw(Bitmap bmp, Graphics g)
    {
        throw new NotImplementedException();
        for (int k = 0; k <lenz; k++)
        {
            for (int i = 0; i < lenx - 1; i++)
            {
                int index = k * lenz + i;
            }
        }
    }
}