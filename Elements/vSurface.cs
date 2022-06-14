using System;
using System.Drawing;

namespace VisualLogic.Elements;

using Utils;

public class vSurface : VisualElement
{
    private Plot3DCamera camera;
    protected double[] data;
    protected int lenx;
    protected int lenz;
    private double minx;
    private double minz;
    private double maxx;
    private double maxz;
    private double resolution;
    private int lastindex = 0;

    private double get(double x, double z)
    {
        if (x < minx || x > maxx || z < minz || z > maxz)
            return double.NaN;
        int i = (int)((x - minx) / resolution),
            k = (int)((z - minz) / resolution);
        this.lastindex = i + k * lenx;
        Update();
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

    public vSurface() : this(0.0, 20.0, 0.0, 5.0, 0.1) { }
    public vSurface(
        double minx, double maxx, 
        double minz, double maxz,
        double resolution)
    {
        this.lenx = (int)((maxx - minx) / resolution);
        this.lenz = (int)((maxz - minz) / resolution);
        this.data = new double[this.lenx * this.lenz];
        this.minx = minx;
        this.minz = minz;
        this.maxx = maxx;
        this.maxz = maxz;
        this.resolution = resolution;

        double cx = (maxx + minx) / 2;
        double cz = -0.5;
        double cy = 2.5;

        camera = new Plot3DCamera((cx, cy, cz),
            Vector.uZ, 0.5, Vector.uX, Vector.uY);
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
        g.Clear(Color.White);
        int wid = bmp.Width / 2, hei = bmp.Height / 2;
        for (int k = lenz - 2; k > -1; k--)
        {
            for (int i = 0; i < lenx - 1; i++)
            {
                int index = k * lenx + i;
                Vector u = (minx + i * resolution, data[index], minz + k * resolution),
                       v = (minx + (i + 1) * resolution, data[index + 1], minz + k * resolution),
                       w = (minx + i * resolution, data[index + lenx], minz + (k + 1) * resolution);
                double max = Math.Max(u.Y, Math.Max(v.Y, w.Y));
                double min = Math.Min(u.Y, Math.Max(v.Y, w.Y));
                float colorcoef = (float)((max - min) / max);
                if (max == 0)
                    colorcoef = 0f;
                Color color = Color.FromArgb((int)(255 - 64 * colorcoef), 
                    (int)(64 * colorcoef), (int)(64 * colorcoef));
                camera.FillPolygon(g, new SolidBrush(color), wid, hei, 200f, u, v, w);
                camera.DrawPolygon(g, Pens.Black, wid, hei, 200f, u, v, w);

                u = (minx + (i + 1) * resolution, data[index + 1 + lenx], minz + (k + 1) * resolution);
                max = Math.Max(u.Y, Math.Max(v.Y, w.Y));
                min = Math.Min(u.Y, Math.Max(v.Y, w.Y));
                colorcoef = (float)((max - min) / max);
                if (max == 0)
                    colorcoef = 0f;
                color = Color.FromArgb((int)(255 - 64 * colorcoef), 
                    (int)(64 * colorcoef), (int)(64 * colorcoef));
                camera.FillPolygon(g, new SolidBrush(color), wid, hei, 200f, u, v, w);
                camera.DrawPolygon(g, Pens.Black, wid, hei, 200f, u, v, w);
            }
        }

        Vector p = (resolution * (lastindex % lenx), data[lastindex] ,resolution * (lastindex / lenx));
        camera.FillPoint(g, Brushes.Blue, wid, hei, 200f, p);
    }
}