using System.Drawing;

namespace VisualLogic.Elements;

using Utils;

public class vArray : VisualElement
{
    protected float[] data;
    protected PlotBar plot;

    public vArray() : this(0, 1000, 50) { }
    public vArray(int min, int max, int values)
    {
        data = new float[values];
        this.plot = new PlotBar();
        this.plot.Max = max;
        this.plot.Min = min;
    }

    public virtual float this[int i]
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