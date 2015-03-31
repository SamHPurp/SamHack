using System;
using System.IO;

public static class PsuedoRandom
{
    public static void GenerateSeed()
    {

    }

	public static void ShowRandomNumbers(int seed)
    {
        Random spork = new Random(seed);

        for (int x = 0; x < 10; x++)
        {
            Action.Display(spork.Next());
        }
    }
}