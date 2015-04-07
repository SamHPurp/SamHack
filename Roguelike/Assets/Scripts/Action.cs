using UnityEngine;
using System.Collections;

public static class Action
{
    public static void OpenDoor(Transform thisLocation, Generation mapGenerator, Vector3 direction)
    {
        Tile potentialLocation = mapGenerator.GetTile((int)thisLocation.position.x + (int)direction.x, (int)thisLocation.position.y + (int)direction.y);
        if(IsActionPossible(thisLocation.position + direction, mapGenerator, potentialLocation, Tile.TileType.ClosedDoor, Tile.TileType.OpenDoor))
        {
            mapGenerator.GetTile((int)potentialLocation.transform.position.x, (int)potentialLocation.transform.position.y).UpdateTileType(Tile.TileType.OpenDoor);
        }
        GameFlow.ExecuteTurn();
    }

    public static void CloseDoor(Transform thisLocation, Generation mapGenerator, Vector3 direction)
    {
        Tile potentialLocation = mapGenerator.GetTile((int)thisLocation.position.x + (int)direction.x, (int)thisLocation.position.y + (int)direction.y);
        if (IsActionPossible(thisLocation.position + direction, mapGenerator, potentialLocation, Tile.TileType.OpenDoor, Tile.TileType.ClosedDoor))
        {
            mapGenerator.GetTile((int)potentialLocation.transform.position.x, (int)potentialLocation.transform.position.y).UpdateTileType(Tile.TileType.ClosedDoor);
        }
        GameFlow.ExecuteTurn();
    }

    public static void UseStairs(Transform thisLocation, Generation mapGenerator, GameManager theManager)
    {
        Tile currentTile = mapGenerator.GetTile((int)thisLocation.transform.position.x, (int)thisLocation.transform.position.y);
        if (currentTile.tileType == Tile.TileType.DownStairs)
        {
            mapGenerator.levels[mapGenerator.currentLevel].SaveMap(mapGenerator.tileScript, true);
            mapGenerator.currentLevel++;
            mapGenerator.DisplayMap(mapGenerator.currentLevel, true);
        }
        if (currentTile.tileType == Tile.TileType.UpStairs)
        {
            if (mapGenerator.currentLevel == 0)
            {
                theManager.GameOver();
            }
            else
            {
                mapGenerator.levels[mapGenerator.currentLevel].SaveMap(mapGenerator.tileScript, false);
                mapGenerator.currentLevel--;
                mapGenerator.DisplayMap(mapGenerator.currentLevel, false);
            }
        }
        GameFlow.ExecuteTurn();
    }

    static bool IsActionPossible(Vector3 newLocation, Generation mapGenerator, Tile tile, Tile.TileType type, Tile.TileType negativeType) // Overflows
    {
        if (tile.tileType == type)
        {
            return true;
        }
        else if (tile.tileType == negativeType)
        {
            Debug.Log("This is already the case"); // Message for display in future?
        }
        return false;
    }

    static bool IsActionPossible(Generation mapGenerator, Tile tile) // Overflows
    {
        if (tile.tileType == Tile.TileType.ClosedDoor)
        {
            return true;
        }
        else if (tile.tileType == Tile.TileType.OpenDoor)
        {
            Debug.Log("This is already the case"); // Message for display in future?
        }
        return false;
    }
}