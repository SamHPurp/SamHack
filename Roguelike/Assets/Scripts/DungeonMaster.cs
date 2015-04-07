using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DungeonMaster
{
    // TODO: Take into account size of open floor space in total of monster generation

    static Generation mapGenerator;
    static GameManager theManager;
    static List<Monster> currentMonsters = new List<Monster>();

    public static void CreateInitialMonsters(Level level)
    {
        for(int i = 0; i < level.maxMonsters; i++)
        {
            level.levelsMonsters.Add(mapGenerator.MonsterLocation(level));
        }
    }

    public static void SpawnMonsters(Level level)
    {
        Monster[] monster = currentMonsters.ToArray();

        for (int i = 0; i < monster.Length; i++)
        {
            theManager.RemoveMonster(monster[i]);
        }

        currentMonsters.Clear();

        foreach(Vector2 v2 in level.levelsMonsters)
        {
            currentMonsters.Add(theManager.TheMonster(v2));
        }
    }

    public static void Register(Generation me)
    {
        mapGenerator = me;
    }
    public static void Register(GameManager me)
    {
        theManager = me;
    }
}