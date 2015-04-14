using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static List<Game> savedGames = new List<Game>();

    public static void SaveGame()
    {
        savedGames.Add(Game.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.sam");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Flush();
        file.Close();
    }

    public static void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/savedGames.sam"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sam", FileMode.Open);
            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
        for (int i = 0; i < 30; i++)
        {
            DungeonMaster.mapGenerator.levels.Add(savedGames[0].levels[i]);
        }

        DungeonMaster.theManager.BuildPlayer();
        DungeonMaster.mapGenerator.DisplayMap(Game.currentLevel, true);
    }
}