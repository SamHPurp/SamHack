using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level
{
    public int width,
               height,
               startingX,
               startingY,
               levelNumber,
               maxMonsters;

    Generation mapGenerator;
    public bool built;
    public System.Random rnd;

    public Tile stairsUp,
                stairsDown;

    public List<Tile> openFloorSpace = new List<Tile>();
    public List<Vector2> levelsMonsters = new List<Vector2>();

    public Tile.TileType[,] tileContents = new Tile.TileType[Generation.mapWidth, Generation.mapHeight];

    public Level(Generation theGen, int lvlNumber)
    {
        rnd = new System.Random();
        levelNumber = lvlNumber;
        maxMonsters = 3 + levelNumber;
        mapGenerator = theGen;
    }

    public void SaveMap(Tile[,] tileScript, bool down)
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