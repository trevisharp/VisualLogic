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

    public Plot3DCamera(Vector position, Vector direction, double focaldistance)
    {
        this.camera = position;
        this.dir = direction;
        this.focal = focaldistance;
    }

    public void DrawPolygon(Pen pen, params Vector[] points)
    {

    }

    private void updateplane()
    {
        this.plane = new Plane(
            camera + dir / dir.Mod * focal, dir);
    }

    private PointF vectortoscreen(Vector vector)
    {
        var r = Camera - vector;
        var p0 = Camera + CameraDirection * FocalDistance;
    }
}