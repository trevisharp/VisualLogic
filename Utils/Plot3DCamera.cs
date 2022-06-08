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

    public void DrawPolygon(Graphics g, Pen pen, params Vector[] points)
    {
        g.DrawPolygon(pen, points.Select(v => vectortoscreen(v)).ToArray());
    }

    public void FillPolygon(Graphics g, Brush brush, params Vector[] points)
    {
        g.FillPolygon(brush, points.Select(v => vectortoscreen(v)).ToArray());
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
        var soltuion = ls.SolveVectors(d, this.CameraVectorX, this.CameraVectorY);
        return new PointF((float)soltuion[0], (float)soltuion[1]);
    }
}