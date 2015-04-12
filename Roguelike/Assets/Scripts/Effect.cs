using UnityEngine;
using System.Collections;

public static class Effect
{
	public static void TakeDamage(Actor actor, int damage)
    {
        actor.myStats.AlterHealth(CalculateDamageTaken(damage));
    }

    static int CalculateDamageTaken(int damage)
    {
        return damage;
    }
}