using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation : MonoBehaviour
{
    //TODO: Refactor the rectangles into Rect Structs

    const int mapWidth = 30,
              mapHeight = 25;

    public GameManager theManager;
    GameObject theCamera,
               thePlayer,
               tilePrefab;
    GameObject[,] tiles = new GameObject[mapWidth, mapHeight];
    Tile[,] tileScript = new Tile[mapWidth, mapHeight];
    List<Tile> possibleFeatureLoctions = new List<Tile>();
    List<Tile> openFloorSpace = new List<Tile>();

    Tile possibleDoor,
         stairsUp,
         stairsDown;

    Vector2[] directions = new Vector2[4];
    int buildDirection;
	
    void Awake()
    {
        this.tag = "MapGeneration";

        if (tilePrefab == null)
        {
            tilePrefab = (GameObject)Resources.Load("Prefabs/Tile");
        }

        directions[0] = new Vector2(1, 0); // look right
        directions[1] = new Vector2(0, -1); // look down
        directions[2] = new Vector2(-1, 0); // look left
        directions[3] = new Vector2(0, 1); // look up
    }
	
	void Start()
	{
        GenerateMap();
	}

    public void GenerateTheRooms(int numberOfRooms)
    {
        WipeMap();
        CreateFirstRoom();
        for (int i = 0; i < numberOfRooms; i++)
        {
            CreateNewFeature();
        }

        GenerateExitsAndEntrance();
    }

    private void GenerateMap()
    {
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                tiles[x, y] = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tiles[x, y].transform.parent = this.transform;
                tiles[x, y].name = "Tile(" + x + "," + y + ")";
                tileScript[x, y] = tiles[x, y].GetComponent<Tile>();
                tileScript[x, y].tileLocation = new Vector2(x,y);
            }
        }

        WipeMap();
    }

    public void SpawnPlayer(GameObject playerPrefab, GameObject cameraPrefab)
    {
        Destroy(thePlayer);
        Destroy(theCamera);
        thePlayer = (GameObject)Instantiate(playerPrefab,new Vector2(stairsUp.tileLocation.x,stairsUp.tileLocation.y),Quaternion.identity);
        thePlayer.name = "Player";
        theCamera = (GameObject)Instantiate(cameraPrefab, new Vector3(stairsUp.tileLocation.x, stairsUp.tileLocation.y, -10), Quaternion.identity);
        theCamera.name = "Main Camera";
        theManager.RegisterPlayer(thePlayer, theCamera);
    }

    private void WipeMap()
    {
        possibleFeatureLoctions.Clear();
        openFloorSpace.Clear();
        FillRect(0, 0, mapWidth, mapHeight, Tile.TileType.SolidRock);
    }

    private void GenerateExitsAndEntrance()
    {
        bool stairsBuilt = false;

        while(stairsBuilt == false)
        {
            List<Tile> potentialExitsAndEntrances = new List<Tile>(openFloorSpace);
            stairsUp = openFloorSpace[Random.Range(0, openFloorSpace.Count)];

            for (int i = 0; i < potentialExitsAndEntrances.Count; i++)
            {
                stairsDown = openFloorSpace[Random.Range(0, openFloorSpace.Count)];
                if (stairsDown.tileLocation.x > stairsUp.tileLocation.x + 5 || stairsDown.tileLocation.x < stairsUp.tileLocation.x - 5)
                {
                    if (stairsDown.tileLocation.y > stairsUp.tileLocation.y + 5 || stairsDown.tileLocation.y < stairsUp.tileLocation.y - 5)
                    {
                        stairsUp.UpdateTileType(Tile.TileType.Stairs);
                        stairsDown.UpdateTileType(Tile.TileType.Stairs);
                        stairsBuilt = true;
                        break;
                    }
                    else
                    {
                        potentialExitsAndEntrances.Remove(stairsDown);
                    }
                }
            }
            if(stairsBuilt)
            {
                break;
            }
        }
    }

    private void FillRect(int startingX, int startingY, int width, int height, Tile.TileType tileType)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((startingX + x >= 0 && startingX + x < mapWidth) && (startingY + y >= 0 && startingY + y < mapHeight))
                {
                    tileScript[startingX + x, startingY + y].UpdateTileType(tileType);
                    if (tileType == Tile.TileType.Floor)
                    {
                        openFloorSpace.Add(tileScript[startingX + x, startingY + y]);
                    }
                }
            }
        }
    }

    private void CreateFirstRoom()
    {
        int roomWidth  = Random.Range(4, 8),
            roomHeight = Random.Range(3, 6),
            startingX = Random.Range(0, mapWidth - roomWidth),
            startingY  = Random.Range(0, mapHeight - roomHeight);
        ConstructRoom(startingX, startingY, roomWidth, roomHeight, Tile.TileType.Floor);
    }

    private void ConstructRoom(int startingX, int startingY, int roomWidth, int roomHeight, Tile.TileType theTileType)
    {
        if (IsBuildSpaceClear(startingX, startingY, roomWidth, roomHeight))
        {
            FillRect(startingX, startingY, roomWidth, roomHeight, theTileType);
            CollectPossiblePaths(startingX, startingY, roomWidth, roomHeight);
        }
    }

    public void CreateNewFeature() //note overflows
    {
        possibleDoor = BuildDirection(SelectRandomSpot());
        if (possibleDoor != null)
        {
            if(Random.Range(0,2) == 0)
            {
                possibleDoor.UpdateTileType(Tile.TileType.ClosedDoor);
            }
            else
            {
                possibleDoor.UpdateTileType(Tile.TileType.OpenDoor);
            }
            SelectNewFeature(possibleDoor);
            possibleFeatureLoctions.Remove(possibleDoor);
        }
        else
        {
            possibleFeatureLoctions.Remove(possibleDoor);
            CreateNewFeature();
        }
    }

    public void CreateNewFeature(Tile theCurrentTile) //note overflows
    {
        possibleDoor = BuildDirection(theCurrentTile);
        if (possibleDoor != null)
        {
            if (Random.Range(0, 2) == 0)
            {
                possibleDoor.UpdateTileType(Tile.TileType.ClosedDoor);
            }
            else
            {
                possibleDoor.UpdateTileType(Tile.TileType.OpenDoor);
            }
            SelectNewFeature(possibleDoor);
            possibleFeatureLoctions.Remove(possibleDoor);
        }
        else
        {
            possibleFeatureLoctions.Remove(possibleDoor);
            CreateNewFeature(theCurrentTile);
        }
    }

    private void SelectNewFeature(Tile door)
    {
        //TODO: Switch statement

        int roomWidth = Random.Range(4, 8),
            roomHeight = Random.Range(3, 6),
            tileX = (int)door.transform.localPosition.x,
            tileY = (int)door.transform.localPosition.y,
            widthModifer = Random.Range(0,roomWidth),
            heightModifer = Random.Range(0,roomHeight),
            featureNumber = Random.Range(0,2);

        switch(featureNumber)
        {
            case 0:
                BuildNewRoom(roomWidth, roomHeight, tileX, tileY, widthModifer, heightModifer);
                break;

            case 1:
                //BuildShortPath(roomWidth, roomHeight, tileX, tileY);
                BuildNewRoom(roomWidth, roomHeight, tileX, tileY, widthModifer, heightModifer);
                break;
                
            default:
                Debug.Log("Error with feature-switch statement");
                break;
        }

    }

    private void BuildShortPath(int pathWidth, int pathHeight, int tileX, int tileY)
    {
        if(buildDirection == 0)
        {
            if(tileX + pathWidth < mapWidth)
            {
                ConstructRoom(tileX,tileY,pathWidth,1, Tile.TileType.Floor);
            }
        }
    }

    private void BuildNewRoom(int roomWidth, int roomHeight, int tileX, int tileY, int widthModifer, int heightModifer)
    {
        if (buildDirection == 0)
        {
            if ((tileX > 0 && tileX < mapWidth) && (tileY - heightModifer > 0 && tileY < mapHeight))
            {
                ConstructRoom(tileX + 1, tileY - heightModifer, roomWidth, roomHeight, Tile.TileType.Floor);
            }
        }

        if (buildDirection == 1)
        {
            if ((tileX - widthModifer > 0 && tileX < mapWidth) && (tileY > 0 && tileY < mapHeight))
            {
                ConstructRoom(tileX - widthModifer, tileY - roomHeight, roomWidth, roomHeight, Tile.TileType.Floor);
            }
        }

        if (buildDirection == 2)
        {
            if (tileX - roomWidth - 1 > 0 && (tileY - heightModifer > 0 && tileY < mapHeight))
            {
                ConstructRoom(tileX - roomWidth, tileY - heightModifer, roomWidth, roomHeight, Tile.TileType.Floor);
            }
        }

        if (buildDirection == 3)
        {
            if ((tileX - widthModifer > 0 && tileX < mapWidth) && (tileY < mapHeight))
            {
                ConstructRoom(tileX - widthModifer, tileY + 1, roomWidth, roomHeight, Tile.TileType.Floor);
            }
        }
    }

    private void CollectPossiblePaths(int sx, int sy, int j, int k)
    {
        for (int x = 0; x < j; x++)
        {
            for(int y = 0; y < k; y++)
            {
                if(x == 0 || y == 0 || x == j - 1 || y == k - 1)
                {
                    possibleFeatureLoctions.Add(tileScript[sx + x, sy + y]);
                }
            }
        }
    }

    private bool IsBuildSpaceClear(int sx, int sy, int j, int k)
    {
        if ((sx + j < mapWidth && sx + j >= 0) && (sy + k < mapHeight && sy + k >= 0) && (sx >= 0 && sy >=0))
        {
            for (int x = -1; x < j+1; x++)
            {
                for (int y = -1; y < k+1; y++)
                {
                    if ((x + sx >= 0 && x + sx < mapWidth) && (y + sy >= 0 && y + sy < mapHeight))
                    {
                        if (tileScript[x + sx, y + sy].tileType != Tile.TileType.SolidRock 
                            && tileScript[x +sx, y + sy].tileType != Tile.TileType.ClosedDoor
                            && tileScript[x + sx, y + sy].tileType != Tile.TileType.OpenDoor)
                        {
                            possibleDoor.UpdateTileType(Tile.TileType.SolidRock);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        else
        {
            possibleDoor.UpdateTileType(Tile.TileType.SolidRock);
            return false;
        }
    }

    private Tile SelectRandomSpot()
    {
        Tile randomedTile = possibleFeatureLoctions[Random.Range(0, possibleFeatureLoctions.Count)];

        return randomedTile;
    }

    private Tile BuildDirection(Tile potentialBuildSpot)
    {
        int tileX = (int)potentialBuildSpot.transform.localPosition.x;
        int tileY = (int)potentialBuildSpot.transform.localPosition.y;

        if((tileX != mapWidth && tileY != mapHeight) || (tileX != 0 && tileY != mapHeight) || (tileX != mapWidth && tileY != 0) || (tileX != 0 && tileY != 0))
        {
            for (int i = 0; i < directions.Length; i++)
            {
                if (tileX + directions[i].x < mapWidth && tileY + directions[i].y < mapHeight && tileX + directions[i].x > 0 && tileY + directions[i].y > 0)
                {
                    if (tileScript[tileX + (int)directions[i].x, tileY + (int)directions[i].y].tileType == Tile.TileType.SolidRock)
                    {
                        buildDirection = i;
                        return tileScript[tileX + (int)directions[i].x, tileY + (int)directions[i].y];
                    }
                }
            }
        }
        return null;
    }

    public Tile GetTile(int x, int y)
    {
        return tileScript[x,y];
    }
}