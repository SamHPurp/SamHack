using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DungeonMaster
{
    // TODO: Take into account size of open floor space in total of monster generation

    public static Generation mapGenerator;
    public static GameManager theManager;
    public static List<Monster> currentMonsters = new List<Monster>();

    public static void CreateInitialMonsters(Level level)
    {
        for(int i = 0; i < level.maxMonsters; i++)
        {
            Point monsterSpawn = mapGenerator.MonsterLocation(level);
            level.monsterSpawns.Add(monsterSpawn);
        }
    }

    public static void SpawnMonsters(Level level)
    {
        Monster[] monster = currentMonsters.ToArray();

        for (int i = 0; i < monster.Length; i++)
            theManager.RemoveMonster(monster[i]);

        currentMonsters.Clear();

        if (level.monsterSpawns.Count > 0)
        {
            for (int i = 0; i < level.monsterSpawns.Count; i++)
            {
                Monster builtMonster = (Monster)ScriptableObject.CreateInstance("Monster");
                builtMonster.myGO.name = "Monster";
                theManager.PlaceMonster(builtMonster, level.monsterSpawns[i]);
                level.levelsMonsters.Add(builtMonster);
                level.monsterSpawns.Remove(level.monsterSpawns[i]);
            }
            currentMonsters = level.levelsMonsters;
        }
        else
            foreach (Monster monsters in level.levelsMonsters)
                currentMonsters.Add(monsters);
    }

    public static void MonsterSlain(Actor monster)
    {
        currentMonsters.Remove((Monster)monster);
        theManager.RemoveMonster(monster);
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