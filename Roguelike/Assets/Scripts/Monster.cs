using UnityEngine;
using System.Collections;

[System.Serializable]
public class Monster : Actor
{
    public Monster(Point myLocation)
    {
        myGO.transform.position = myLocation.ToVector2();
    }

    public Monster(Vector2 myLocation)
    {
        myGO.transform.position = myLocation;
    }

    void Awake()
    {
        myStats = new ActorStats(this);
    }

	public override void TakeTurn()
    {
        Vector3 direction = mapGenerator.directions[Random.Range(0,mapGenerator.directions.Length)];
        Movement.Move(location, direction, -Vector3.zero);
    }
}