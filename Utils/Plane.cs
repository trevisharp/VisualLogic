using System.Drawing;

namespace VisualLogic.Utils;

public struct Plane
{
    public static Plane Empty => new Plane(1.0, 0, 0, 0);
    public Plane(double a, double b, double c, double d)
    {
        this.A = a;
        this.B = b;
        this.C = c;
        this.D = d;
    }

    public Plane(Vector normal, Vector point)
    {
        this.A = normal.X;
        this.B = normal.Y;
        this.C = normal.Z;
        this.D = -(this.A * point.X + this.B * point.Y + this.C * point.Z);
    }
    
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }
    public double D { get; set; }

    public Vector Intersection(Vector vec, Vector pnt)
    {
        //a (px - t vx) + b (py - t vy) + c (pz - t vz) + d = 0
        //a px + b py + c pz + d = t (vx + vy + vz)
        //t = (a px + b py + c pz + d) / (vx + vy + vz)
        //p + t * vec = intersection point
        double t = (A * pnt.X + B * pnt.Y + C * pnt.Z) / (vec.X + vec.Y + vec.Z);
        return pnt + t * vec;
    }
}