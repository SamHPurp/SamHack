using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tile : MonoBehaviour
{
    [System.NonSerialized]
    Renderer thisRenderer;

    [System.NonSerialized]
    public bool walkable;

    [System.NonSerialized]
    public GameObject occupied;

    [System.NonSerialized]
    public Generation theGenerator;

    [System.NonSerialized]
    public Vector2 tileLocation;

    void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        theGenerator = FindObjectOfType<Generation>();
    }

    [System.Serializable]
    public enum TileType { Floor, Corridor, Wall, SolidRock, OpenDoor, ClosedDoor, UpStairs, DownStairs };

    public TileType tileType;

    public void CreateFeature(Tile theTile)
    {
        Debug.Log("Manually creating new feature");
        theGenerator.CreateNewFeature(theTile);
    }

    public void UpdateTileType(TileType newTileSet)
    {
        tileType = newTileSet;
        switch(newTileSet)
        {
            case TileType.Floor:
                {
                    thisRenderer.material.color = Color.green;
                    walkable = true;
                    break;
                }
            case TileType.Corridor:
                {
                    thisRenderer.material.color = Color.magenta;
                    walkable = true;
                    break;
                }
            case TileType.Wall:
                {
                    thisRenderer.material.color = Color.blue;
                    walkable = false;
                    break;
                }
            case TileType.SolidRock:
                {
                    thisRenderer.material.color = Color.black;
                    walkable = false;
                    break;
                }
            case TileType.OpenDoor:
                {
                    thisRenderer.material.color = Color.cyan;
                    walkable = true;
                    break;
                }
            case TileType.ClosedDoor:
                {
                    thisRenderer.material.color = Color.red;
                    walkable = false;
                    break;
                }
            case TileType.UpStairs:
                {
                    thisRenderer.material.color = Color.yellow;
                    walkable = true;
                    break;
                }
            case TileType.DownStairs:
                {
                    thisRenderer.material.color = Color.white;
                    walkable = true;
                    break;
                }
            default:
                {
                    Debug.Log("<color=red>Incorrect TileType Assignment</color>");
                    break;
                }
        }
    }
}