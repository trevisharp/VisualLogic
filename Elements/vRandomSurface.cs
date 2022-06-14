using System;

namespace VisualLogic.Elements;

public class vRandomSurface : vSurface
{
    public vRandomSurface(
        double minx, double maxx, 
        double minz, double maxz,
        double resolution) : base(minx, maxx, minz, maxz, resolution)
        {
            Random rand =new Random(DateTime.Now.Millisecond);
            int xmaxy = rand.Next(lenx);
            int zmaxy = rand.Next(lenz);
            for (int k = 0; k < lenz; k++)
            {
                for (int i = 0; i < lenx; i++)
                {
                    int index = k * lenx + i;
                    int dk = k - zmaxy, di = i - xmaxy;
                    double error = resolution * Math.Sqrt(dk * dk + di * di);
                    data[index] = 10.0 / (error + 4.0);
                }
            }
        }
    
    public vRandomSurface() : this(0.0, 20.0, 0.0, 5.0, 0.25) { }
}