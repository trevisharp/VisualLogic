namespace VisualLogic.Utils;

public class LinearSystem
{
    public double[] SolveLinearCombination(Vector u, Vector v, Vector r)
    {
        //a * u + b * v = r     u, v != (0, 0, 0)
        //a * ux + b * vx = rx
        //a * uy + b * vy = ry
        //a * uz + b * vz = rz
        // a = (rx - b * vx) / ux
        // uy / ux (rx - b * vx) + b * vy = ry
        // uy rx / ux + b * (vy - vx uy / ux) = ry
        // b = (ry - uy * rx / ux) / (vy - vx * uy / ux)
        
        double a = 0.0, b = 0.0;
        if (u.X != 0)
        {
            b = (r.Y - u.Y * r.X / u.X) / (v.Y - v.X * u.Y / u.X);
            a = (r.X - b * v.X) / u.X;
        }
        else if (u.Y != 0)
        {
            b = (r.X - u.X * r.Y / u.Y) / (v.X - v.Y * u.X / u.Y);
            a = (r.Y - b * v.Y) / u.Y;
        }
        else
        {
            b = (r.X - u.X * r.Z / u.Z) / (v.X - v.Z * u.X / u.Z);
            a = (r.Z - b * v.Z) / u.Z;
        }

        return new double[2] { a, b };
    }
}