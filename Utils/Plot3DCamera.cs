using System.Linq;
using System.Drawing;

namespace VisualLogic.Utils;

public class Plot3DCamera
{
    private Plane plane = Plane.Empty;

    private Vector camera;
    public Vector Camera
    {
        get => camera;
        set
        {
            camera = value;
            updateplane();
        }
    }

    private Vector dir;
    public Vector CameraDirection
    {
        get => dir;
        set
        {
            dir = value;
            updateplane();
        }
    }

    private double focal;
    public double FocalDistance
    {
        get => focal;
        set
        {
            focal = value;
            updateplane();
        }
    }

    public Vector CameraVectorX { get; set; }
    public Vector CameraVectorY { get; set; }

    public Plot3DCamera(Vector position, Vector direction, 
        double focaldistance, Vector camvecx, Vector camvecy)
    {
        this.camera = position;
        this.dir = direction;
        this.focal = focaldistance;
        this.CameraVectorX = camvecx;
        this.CameraVectorY = camvecy;
    }

    public void DrawPolygon(Graphics g, Pen pen, int wid, int hei, float ppv, params Vector[] points)
    {
        var pts = points.Select(v => vectortoscreen(v)).ToArray();
        pts = pts.Select(p => new PointF(
            ppv * p.X + wid,
            ppv * p.Y + hei
            )).ToArray();
        // throw new System.Exception($"{pts[0]}   {pts[1]}    {pts[2]}");
        g.DrawPolygon(pen, pts);
    }

    public void FillPolygon(Graphics g, Brush brush, Bitmap bmp, float ppv, params Vector[] points)
    {
        var pts = points.Select(v => vectortoscreen(v)).ToArray();
        pts = pts.Select(p => new PointF(
            ppv * p.X + bmp.Width / 2,
            ppv * p.Y + bmp.Height / 2))
            .ToArray();
        g.FillPolygon(brush, pts);
    }

    private void updateplane()
    {
        this.plane = new Plane(
            camera + dir / dir.Mod * focal, dir);
    }

    private PointF vectortoscreen(Vector point)
    {
        var r = point - Camera;
        var p = plane.Intersection(r, Camera);
        var center = plane.Intersection(dir, Camera);
        var d = p - center;
        var ls = new LinearSystem();
        var soltuion = ls.SolveLinearCombination(this.CameraVectorX, this.CameraVectorY, d);
        float a = (float)soltuion[0], b = (float)soltuion[1];
        return new PointF(a, b);
    }
}