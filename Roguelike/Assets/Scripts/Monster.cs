using UnityEngine;
using System.Collections;

public class Monster : Actor
{
    Renderer myRenderer;

    public Monster()
    {

    }

	public override void TakeTurn()
    {
        Vector3 direction = mapGenerator.directions[Random.Range(0,mapGenerator.directions.Length)];
        Movement.Move(location, direction, -Vector3.zero);
    }
}