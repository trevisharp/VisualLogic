using System.Drawing;
using System.Windows.Forms;

namespace VisualLogic;

public class VisualArgs
{
    public Bitmap Bitmap { get; set; } = null;
    public Graphics Graphics { get; set; } = null;
    public Form Form { get; set; } = null;
    public PictureBox PictureBox { get; set; } = null;
    public int Delay { get; set; } = 25;
}