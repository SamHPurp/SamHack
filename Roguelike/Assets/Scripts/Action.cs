using UnityEngine;
using System.Collections;

public static class Action
{
    public static void OpenDoor(Transform thisLocation, Vector3 direction)
    {
        Generation mapGenerator = DungeonMaster.mapGenerator;
        Tile potentialLocation = mapGenerator.GetTile((int)thisLocation.position.x + (int)direction.x, (int)thisLocation.position.y + (int)direction.y);
        if(IsActionPossible(thisLocation.position + direction, mapGenerator, potentialLocation, Tile.TileType.ClosedDoor, Tile.TileType.OpenDoor))
        {
            mapGenerator.GetTile((int)potentialLocation.transform.position.x, (int)potentialLocation.transform.position.y).UpdateTileType(Tile.TileType.OpenDoor);
        }
        GameFlow.ExecuteTurn();
    }

    public static void CloseDoor(Transform thisLocation, Vector3 direction)
    {
        Generation mapGenerator = DungeonMaster.mapGenerator;
        Tile potentialLocation = mapGenerator.GetTile((int)thisLocation.position.x + (int)direction.x, (int)thisLocation.position.y + (int)direction.y);
        if (IsActionPossible(thisLocation.position + direction, mapGenerator, potentialLocation, Tile.TileType.OpenDoor, Tile.TileType.ClosedDoor))
        {
            mapGenerator.GetTile((int)potentialLocation.transform.position.x, (int)potentialLocation.transform.position.y).UpdateTileType(Tile.TileType.ClosedDoor);
        }
        GameFlow.ExecuteTurn();
    }

    public static void UseStairs(Transform thisLocation)
    {
        GameManager theManager = DungeonMaster.theManager;
        Generation mapGenerator = DungeonMaster.mapGenerator;
        Tile currentTile = DungeonMaster.mapGenerator.GetTile((int)thisLocation.transform.position.x, (int)thisLocation.transform.position.y);
        
        if (currentTile.tileType == Tile.TileType.DownStairs)
        {
            mapGenerator.levels[Game.currentLevel].SaveLevel(mapGenerator.tileScript, true, false);
            Game.currentLevel++;
            mapGenerator.DisplayMap(Game.currentLevel, true);
        }
        if (currentTile.tileType == Tile.TileType.UpStairs)
        {
            if (Game.currentLevel == 0)
            {
                theManager.GameOver();
            }
            else
            {
                mapGenerator.levels[Game.currentLevel].SaveLevel(mapGenerator.tileScript, false, false);
                Game.currentLevel--;
                mapGenerator.DisplayMap(Game.currentLevel, false);
            }
        }
        GameFlow.ExecuteTurn();
    }
    
    public static void AttackMelee(Tile currentTile, Tile attemptTile)
    {
        Debug.Log("Attacking");
        ActorController target = attemptTile.occupied.GetComponent<ActorController>();

        Effect.TakeDamage(target.monsterController, 1);
    }

    public static void AssessAction(Transform thisLocation, Vector3 direction, Vector3 direction2)
    {
        Generation mapGenerator = DungeonMaster.mapGenerator;
        Tile currentTile = mapGenerator.GetTile(thisLocation.transform.position);
        Tile attemptTile = mapGenerator.GetTile(thisLocation.position + direction + direction2);

        if (attemptTile.occupied != null)
        {
            AttackMelee(currentTile, attemptTile);
            GameFlow.ExecuteTurn();
        }
        else if (attemptTile.walkable)
        {
            Movement.Move(thisLocation, direction, direction2);
            GameFlow.ExecuteTurn();
        }
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