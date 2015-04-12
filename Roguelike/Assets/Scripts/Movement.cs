using UnityEngine;
using System.Collections;

public static class Movement
{
    private static Generation mapGenerator;

    public static void RegisterGenerator(Generation me)
    {
        mapGenerator = me;
    }

    public static void Move(Transform thisLocation, Vector3 direction, Vector3 direction2)
    {
        Vector3 newLocation = direction + direction2;
        if (CanWeMoveThisDirection(thisLocation.position + newLocation))
        {
            mapGenerator.GetTile((int)thisLocation.position.x, (int)thisLocation.position.y).occupied = null;
            mapGenerator.GetTile((int)(thisLocation.position.x + newLocation.x), (int)(thisLocation.position.y + newLocation.y)).occupied = thisLocation.gameObject;
            thisLocation.position += newLocation;
        }
    }

    static bool CanWeMoveThisDirection(Vector3 newLocation)
    {
        if (newLocation.x < 0 || newLocation.y < 0 || newLocation.x > Generation.mapWidth || newLocation.y > Generation.mapHeight)
        {
            return false;
        }
        else
        {
            Tile potentialLocation = mapGenerator.GetTile((int)newLocation.x, (int)newLocation.y);
            if (potentialLocation.walkable && potentialLocation.occupied == null)
            {
                return true;
            }
        }
        return false;
    }
}