using System;
using System.Drawing;

namespace VisualLogic.Elements;

using Utils;

public class VisualArray : VisualElement
{
    private float[] data;
    private PlotBar plot;

    public VisualArray(int min, int max, int values)
    {
        data = new float[values];
        Random rand = new Random();
        for (int k = 0; k < values; k++)
            data[k] = rand.Next(min, max + 1);
        this.plot = new PlotBar();
        this.plot.Max = max;
        this.plot.Min = min;
    }

    public float this[int i]
    {
        get => data[i];
        set
        {
            data[i] = value;
            Update();
        }
    }

    public int Length => data.Length;
    protected override void Draw(Bitmap bmp, Graphics g)
    {
        g.Clear(Color.White);
        this.plot.Draw(g, new RectangleF(PointF.Empty, bmp.Size), data);
    }
}