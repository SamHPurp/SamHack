using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level
{
    public int width,
               height,
               startingX,
               startingY;
    public bool built;
    public System.Random rnd;

    public Tile stairsUp,
                stairsDown;

    public List<Rect> rooms = new List<Rect>();

    public Tile.TileType[,] tileContents = new Tile.TileType[Generation.mapWidth, Generation.mapHeight];

    public Level()
    {
        rnd = new System.Random();
    }

    public void SaveMap(Tile[,] tileScript)
    {
        for(int x = 0; x < Generation.mapWidth; x++)
        {
            for(int y= 0; y < Generation.mapHeight; y++)
            {
                tileContents[x, y] = tileScript[x, y].tileType;
            }
        }
    }
}