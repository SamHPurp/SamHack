using UnityEngine;
using System.Collections;

public static class Action
{
    public static void OpenDoor(Transform thisLocation, Generation mapGenerator, Vector3 direction)
    {
        Tile potentialLocation = mapGenerator.GetTile((int)thisLocation.position.x + (int)direction.x, (int)thisLocation.position.y + (int)direction.y);
        if(IsItADoor(thisLocation.position + direction, mapGenerator, potentialLocation))
        {
            mapGenerator.GetTile((int)potentialLocation.transform.position.x, (int)potentialLocation.transform.position.y).UpdateTileType(Tile.TileType.OpenDoor);
        }
    }

    public static void CloseDoor(Transform thisLocation, Generation mapGenerator, Vector3 direction)
    {
        Tile potentialLocation = mapGenerator.GetTile((int)thisLocation.position.x + (int)direction.x, (int)thisLocation.position.y + (int)direction.y);
        if (IsItADoor(thisLocation.position + direction, mapGenerator, potentialLocation))
        {
            mapGenerator.GetTile((int)potentialLocation.transform.position.x, (int)potentialLocation.transform.position.y).UpdateTileType(Tile.TileType.ClosedDoor);
        }
    }

    static bool IsItADoor(Vector3 newLocation, Generation mapGenerator, Tile tile)
    {
        if (tile.tileType == Tile.TileType.ClosedDoor)
        {
            return true;
        }
        else if (tile.tileType == Tile.TileType.OpenDoor)
        {
            Debug.Log("Door already open");
        }
        return false;
    }

    public static void Display(int display)
    {
        Debug.Log(display);
    }
}