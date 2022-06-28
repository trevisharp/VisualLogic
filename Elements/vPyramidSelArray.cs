using System;

namespace VisualLogic.Elements;

public class vPyramidSelArray : vSelArray
{
    public vPyramidSelArray() : this(0, 1000, 50) { }
    public vPyramidSelArray(int min, int max, int values) 
        : base(min, max, values)
    {
        Random rand = new Random();
        int imax = rand.Next(values);
        for (int k = 0; k < values; k++)
        {
            float f = 1f - (imax - k) / (float)values;
            if (f < 0)
                f = -f;
            data[k] = min + f * (max - min);
        }
    }
}