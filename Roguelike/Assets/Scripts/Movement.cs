using UnityEngine;
using System.Collections;

public static class Movement
{
    public static void Move(Transform thisLocation, Generation mapGenerator, CameraControl theMainCamera, PlayerControl player, Vector3 direction) // couple overflows
    {
        if (CanWeMoveThisDirection(thisLocation.position + direction, mapGenerator))
        {
            thisLocation.position += direction;
            theMainCamera.UpdateLocation((int)player.transform.position.x, (int)player.transform.position.y);
        }
    }

    public static void Move(Transform thisLocation, Generation mapGenerator, CameraControl theMainCamera, PlayerControl player, Vector3 direction, Vector3 direction2) // couple overflows
    {
        if (CanWeMoveThisDirection(thisLocation.position + direction + direction2, mapGenerator))
        {
            thisLocation.position += direction + direction2;
            theMainCamera.UpdateLocation((int)player.transform.position.x, (int)player.transform.position.y);
        }
    }

    static bool CanWeMoveThisDirection(Vector3 newLocation, Generation mapGenerator)
    {
        Tile potentialLocation = mapGenerator.GetTile((int)newLocation.x, (int)newLocation.y);
        if (potentialLocation.walkable && !potentialLocation.occupied)
        {
            return true;
        }
        return false;
    }
}