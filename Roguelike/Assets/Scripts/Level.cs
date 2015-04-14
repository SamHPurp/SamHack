using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Level
{
    public int width,
               height,
               startingX,
               startingY,
               levelNumber,
               maxMonsters;

    [System.NonSerialized]
    Generation mapGenerator;

    public bool built;
    public System.Random rnd;

    public Point stairsUp,
                 stairsDown;

    public List<Point> openFloorSpace = new List<Point>();
    public List<Point> monsterSpawns = new List<Point>();

    //[NonSerialized]
    public List<Monster> levelsMonsters = new List<Monster>();

    public Tile.TileType[,] tileContents = new Tile.TileType[Generation.mapWidth, Generation.mapHeight];

    public Level(Generation theGen, int lvlNumber)
    {
        rnd = new System.Random();
        levelNumber = lvlNumber;
        maxMonsters = 3 + levelNumber;
        mapGenerator = theGen;
    }

    public void SaveLevel(Tile[,] tileScript, bool down, bool initial)
    {
        SaveMap(tileScript, down);
        SaveActors(initial);
        SaveItems();
        SaveTraps();
    }

    public void SaveLevel(bool initial)
    {
        //SaveMap(tileScript, down);
        SaveActors(initial);
        SaveItems();
        SaveTraps();
    }

    private void SaveMap(Tile[,] tileScript, bool down)
    {
        for(int x = 0; x < Generation.mapWidth; x++)
            for(int y= 0; y < Generation.mapHeight; y++)
                tileContents[x, y] = tileScript[x, y].tileType;
    }

    private void SaveActors(bool initial)
    {
        if (!initial)
        {
            monsterSpawns.Clear();
            foreach (Monster monster in DungeonMaster.currentMonsters)
                monsterSpawns.Add(monster.locationPoint);
        }
    }

    private void SaveItems()
    {
        //TODO
    }

    private void SaveTraps()
    {
        //TODO
    }
}