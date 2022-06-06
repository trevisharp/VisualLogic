namespace VisualLogic;

using System.Windows.Forms;
using System.Drawing;

public class VisualScreen
{
    Bitmap bmp = null;
    Graphics g = null;
    Timer tm = null;
    Form form = null;
    PictureBox pb = null;

    public int TimerDelay { get; set; } = 25;

    public void Open()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        this.pb = new PictureBox();
        this.pb.Dock = DockStyle.Fill;

        this.tm = new Timer();
        this.tm.Interval = 25;

        this.form = new Form();
        this.form.FormBorderStyle = FormBorderStyle.None;
        this.form.WindowState = FormWindowState.Maximized;
        this.form.Controls.Add(this.pb);
        this.form.KeyPreview = true;
        this.form.KeyDown += (o, e) => 
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        };
        this.form.Load += delegate
        {
            this.bmp = new Bitmap(this.pb.Width, this.pb.Height);
            this.g = Graphics.FromImage(this.bmp);
            this.g.Clear(Color.White);
            this.pb.Image = this.bmp;
            
            VisualArgs args = new VisualArgs();
            args.Bitmap = bmp;
            args.Form = form;
            args.Graphics = g;
            args.PictureBox = pb;
            args.Delay = TimerDelay;
        };
        Application.Run(this.form);
    }
}