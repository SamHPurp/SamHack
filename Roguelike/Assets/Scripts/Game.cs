using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game
{
    public static int currentLevel;

    public static Game current = new Game();
    public List<Level> levels = new List<Level>();
}