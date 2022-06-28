using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace VisualLogic.Utils;

public class PlotSelectBar : PlotBar
{
    public int SelectedIndex { get; set; } = -1;
    public override void Draw(Graphics g, RectangleF rect, IEnumerable<float> values)
    {
        var len = values.Count();
        var size = rect.Width / (3 * len + 1);
        var ppv = (rect.Height - 100) / (Max - Min);
        var it = values.GetEnumerator();
        for (float i = size; i < rect.Width && it.MoveNext(); i += 3 * size)
        {
            var value = (it.Current - Min) / (float)(Max - Min);
            Brush color = new SolidBrush(Color.FromArgb(
                (int)(255 * value),
                0,
                (int)(255 * (1 - value))
            ));
            if (i == SelectedIndex)
                color = Brushes.Green;
            g.FillRectangle(color, i, 
                rect.Height - (it.Current - Min) * ppv - 50,
                2 * size, (it.Current - Min) * ppv + 5);
        }
    }
}