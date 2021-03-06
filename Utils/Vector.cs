using System;

namespace VisualLogic.Utils;

public struct Vector
{
    public static Vector uX => (1.0, 0.0, 0.0);
    public static Vector uY => (0.0, 1.0, 0.0);
    public static Vector uZ => (0.0, 0.0, 1.0);
    public Vector(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public double Mod => Math.Sqrt(X * X + Y * Y + Z * Z);
    public Vector Orto => ((Y + Z), -X, -X);

    public static implicit operator Vector((double x, double y, double z) tuple)
        => new Vector(tuple.x, tuple.y, tuple.z);
    
    public static Vector operator -(Vector v)
        => (-v.X, -v.Y, -v.Z);
    
    public static Vector operator +(Vector v, Vector u)
        => (v.X + u.X, v.Y + u.Y, v.Z + u.Z);
    
    public static Vector operator -(Vector v, Vector u)
        => (v.X - u.X, v.Y - u.Y, v.Z - u.Z);

    public static double operator *(Vector v, Vector u)
        => v.X * u.X + v.Y * u.Y + v.Z * u.Z;

    public static Vector operator *(double a, Vector v)
        => (a * v.X, a * v.Y, a * v.Z);

    public static Vector operator *(Vector v, double a)
        => (a * v.X, a * v.Y, a * v.Z);

    public static Vector operator /(Vector v, double a)
        => (v.X / a, v.Y / a, v.Z / a);

    public override string ToString()
        => $"({X}, {Y}, {Z})";
}