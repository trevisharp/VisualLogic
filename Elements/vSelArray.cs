namespace VisualLogic.Elements;

using Utils;

public class vSelArray : vArray
{
    public vSelArray() : this(0, 1000, 50) { }
    public vSelArray(int min, int max, int values)
    {
        data = new float[values];
        this.plot = new PlotSelectBar();
        this.plot.Max = max;
        this.plot.Min = min;
    }

    public override float this[int i]
    {
        get => base[i];
        set
        {
            var psb = this.plot as PlotSelectBar;
            psb.SelectedIndex = i;
            base[i] = value;
        }
    }
}