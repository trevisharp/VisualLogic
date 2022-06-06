using System;
using System.Drawing;

namespace VisualLogic.Elements;

public class VisualArray : VisualElement
{
    private int max;
    private int min;
    private int[] data;

    public VisualArray(int min, int max, int values)
    {
        data = new int[values];
        Random rand = new Random();
        for (int k = 0; k < values; k++)
            data[k] = rand.Next(min, max + 1);
        this.min = min;
        this.max = max;
    }
    public int this[int i]
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
        var size = bmp.Width / (float)(3 * Length + 1);
        var ppv = (max - min) / (float)(bmp.Height - 100);
        int indx = 0;
        for (float i = size; i < bmp.Width && indx < Length; i += 3 * size, indx++)
        {
            var value = (data[indx] - min) / (float)(max - min);
            var color = new SolidBrush(Color.FromArgb(
                (int)(255 * value),
                0,
                (int)(255 * (1 - value))
            ));
            g.FillRectangle(color, i, bmp.Height - data[indx] * ppv - 50, 2 * size, data[indx] * ppv + 5);
        }
    }
}