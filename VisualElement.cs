using System.Drawing;
using System.Threading;

namespace VisualLogic;

public abstract class VisualElement
{
    public static VisualArgs VisualArguments { get; set; }

    protected void Update()
    {
        lock (VisualArguments.Graphics)
        {
            Draw(VisualArguments.Bitmap, VisualArguments.Graphics);
            VisualArguments.PictureBox.Refresh();
        }
        Thread.Sleep(VisualArguments.Delay);
    }

    protected abstract void Draw(Bitmap bmp, Graphics g);
}