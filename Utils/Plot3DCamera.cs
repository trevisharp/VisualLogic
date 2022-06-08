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

    public void DrawPolygon(Pen pen, params Vector[] points)
    {

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
        double x = 0, y = 0;
        // x * CameraVectorX + y * CameraVectorY = d
        // x * cx.X + y * cy.X = d.X
        // x * cx.Y

        return new PointF((float)x, (float)y);
    }
}