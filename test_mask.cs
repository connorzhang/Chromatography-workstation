using System;

class Program
{
    public static byte BitByBitOr(byte byte_1, int int_0)
    {
        byte_1 |= (byte)(1 << int_0);
        return byte_1;
    }

    static void Main()
    {
        bool insDevEnable0 = true; // Inj1
        bool insDevEnable1 = true; // Col
        bool insDevEnable2 = true; // Det1
        bool insDevEnable3 = false; // Det2
        bool insDevEnable4 = false; // Inj2
        bool insDevEnable5 = false; // Det3
        
        byte b = 0;
        if (insDevEnable5) b = BitByBitOr(b, 0);
        if (insDevEnable4) b = BitByBitOr(b, 1);
        if (insDevEnable3) b = BitByBitOr(b, 2);
        if (insDevEnable2) b = BitByBitOr(b, 3);
        if (insDevEnable1) b = BitByBitOr(b, 4);
        if (insDevEnable0) b = BitByBitOr(b, 5);
        
        Console.WriteLine("Mask: " + b + " (0x" + b.ToString("X2") + ")");
    }
}
