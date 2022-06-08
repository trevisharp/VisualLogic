using System;
using System.Drawing;

namespace VisualLogic.Elements;

public class VisualSurface : VisualElement
{
    private double cx, cy, cz;
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

        this.cx = (maxx + minx) / 2;
        this.cz = (maxz + minz) / 2;
        this.cz = 5.0;

        Random rand =new Random(DateTime.Now.Millisecond);
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
        for (int k = 0; k <lenz; k++)
        {
            for (int i = 0; i < lenx - 1; i++)
            {
                int index = k * lenz + i;
            }
        }
    }



    private void drawtriangule(Graphics g, float x1, float y1, float x2, float y2, float x3, float y3)
    {
        g.DrawPolygon(Pens.Black, new PointF[]
        {
            new PointF(x1, y1),
            new PointF(x2, y2),
            new PointF(x3, y3)
        });
    }
}