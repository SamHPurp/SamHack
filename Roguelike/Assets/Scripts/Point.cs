using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point
{
    public int x, y;

    public Point(int setX, int setY)
    {
        x = setX;
        y = setY;
    }

    public Point(Vector2 v2)
    {
        x = (int)v2.x;
        y = (int)v2.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x,y);
    }
}