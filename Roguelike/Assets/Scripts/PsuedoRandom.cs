using System;
using System.IO;

public static class PsuedoRandom
{
    public static Random SetRandom()
    {
        Random rnd = new Random();
        return rnd;
    }

    public static int GenerateRandomNumber(int seed, int min, int max)
    {
        return new Random(seed).Next(min, max);
    }
}