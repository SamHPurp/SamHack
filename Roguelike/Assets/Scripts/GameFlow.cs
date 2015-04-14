using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameFlow
{
    public static List<Actor> actors = new List<Actor>();
    public static int turn = 0;

    public static void RegisterAsActor(Actor me)
    {
        actors.Add(me);
    }

    public static void UnregisterAsActor(Actor me)
    {
        actors.Remove(me);
    }

    public static void ExecuteTurn()
    {
        turn++;
        foreach(Actor actor in actors)
            actor.TakeTurn();
    }
}