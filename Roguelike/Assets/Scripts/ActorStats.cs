using UnityEngine;
using System.Collections;

public class ActorStats
{
    int health = 3;
    Actor me;
    public ActorStats(Actor actor)
    {
        me = actor;
    }

    public void AlterHealth(int damage)
    {
        health = health - damage;

        if(health == 0)
        {
            Debug.Log("Slain");
            DungeonMaster.MonsterSlain(me);
        }
    }
}