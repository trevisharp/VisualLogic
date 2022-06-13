using System;

namespace VisualLogic.Elements;

public class vRandomArray : vArray
{
    public vRandomArray() : this(0, 1000, 50) { }
    public vRandomArray(int min, int max, int values) 
        : base(min, max, values)
    {
        Random rand = new Random();
        for (int k = 0; k < values; k++)
            data[k] = rand.Next(min, max + 1);
    }
}