using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    Renderer thisRenderer;
    public bool occupied,
                walkable;

    public Generation theGenerator;

    public Vector2 tileLocation;

    void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        theGenerator = FindObjectOfType<Generation>();
    }

    public enum TileType { Floor, Corridor, Wall, SolidRock, OpenDoor, ClosedDoor, Stairs };
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
            case TileType.Stairs:
                {
                    thisRenderer.material.color = Color.yellow;
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