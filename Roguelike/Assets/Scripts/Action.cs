using UnityEngine;
using System.Collections;

public static class Action
{
    public static void OpenDoor(Transform thisLocation, Generation mapGenerator, Vector3 direction)
    {
        if(IsItADoor(thisLocation.position + direction, mapGenerator))
        {

        }
    }

    public static void CloseDoor()
    {

    }

    static bool IsItADoor(Vector3 newLocation, Generation mapGenerator)
    {
        Tile potentialLocation = mapGenerator.GetTile((int)newLocation.x, (int)newLocation.y);
        if (potentialLocation.tileType == Tile.TileType.ClosedDoor)
        {
            return true;
        }
        else if(potentialLocation.tileType == Tile.TileType.OpenDoor)
        {
            Debug.Log("Door already open");
        }
        return false;
    }
}