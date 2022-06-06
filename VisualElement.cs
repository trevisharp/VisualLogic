using System.Drawing;
using System.Threading;

namespace VisualLogic;

public abstract class VisualElement
{
    private VisualArgs args = null;

    protected void Update()
    {
        lock (args.Graphics)
        {
            Draw(args.Bitmap, args.Graphics);
            args.PictureBox.Refresh();
        }
        Thread.Sleep(args.Delay);
    }

    protected abstract void Draw(Bitmap bmp, Graphics g);
}