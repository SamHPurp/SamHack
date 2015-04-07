using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Generation : MonoBehaviour
{
    //TODO: Refactor the rooms into Rect Structs
    //TODO: Create Class Room, and save them to the level
    //TODO: Stop minions from being persistant over levels
    //BUG: Minions still walk over each other occasionally... (maybe just on stairs?)

    public const int mapWidth = 30,
                     mapHeight = 25;

    public GameManager theManager;
    GameObject theCamera,
               thePlayer,
               tilePrefab;
    GameObject[,] tiles = new GameObject[mapWidth, mapHeight];
    public Tile[,] tileScript = new Tile[mapWidth, mapHeight];
    public List<Level> levels = new List<Level>();
    List<Tile> possibleFeatureLoctions = new List<Tile>();

    Tile possibleDoor;
    Level buildingLevel;

    public Vector2[] directions = new Vector2[4];
    int buildDirection;
    public int currentLevel = 0,
               seed;
    System.Random rnd;
	
    void Awake()
    {
        this.tag = "MapGeneration";

        Movement.RegisterGenerator(this);
        DungeonMaster.Register(this);

        if (tilePrefab == null)
        {
            tilePrefab = (GameObject)Resources.Load("Prefabs/Tile");
        }

        directions[0] = new Vector2(1, 0); // look right
        directions[1] = new Vector2(0, -1); // look down
        directions[2] = new Vector2(-1, 0); // look left
        directions[3] = new Vector2(0, 1); // look up

        rnd = new System.Random();
        seed = rnd.Next();
    }
	
	void Start()
	{
        GenerateField();
	}

    public void BuildAllMaps(GameObject playerPrefab, GameObject cameraPrefab)
    {
        for(int i = 0; i < 30; i++)
        {
            levels.Add(new Level(this, i));
            buildingLevel = levels[i];
            GenerateTheRooms(30);
            DungeonMaster.CreateInitialMonsters(levels[i]);
            buildingLevel.SaveMap(tileScript, true);
        }
        BuildPlayer(playerPrefab, cameraPrefab);
        DisplayMap(0, true);
    }

    public void DisplayMap(int loadLevel, bool down)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tileScript[x, y].UpdateTileType(levels[loadLevel].tileContents[x, y]);
            }
        }
        SpawnPlayer(down);
        DungeonMaster.SpawnMonsters(levels[loadLevel]);
    }

    private void GenerateField()
    {
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                tiles[x, y] = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tiles[x, y].transform.parent = this.transform;
                tiles[x, y].name = "Tile(" + x + "," + y + ")";
                tileScript[x, y] = tiles[x, y].GetComponent<Tile>();
                tileScript[x, y].tileLocation = new Vector2(x, y);
            }
        }

        WipeMap();
    }
    
    private void BuildPlayer(GameObject playerPrefab, GameObject cameraPrefab)
    {
        thePlayer = (GameObject)Instantiate(playerPrefab, new Vector2(levels[currentLevel].stairsUp.tileLocation.x, levels[currentLevel].stairsUp.tileLocation.y), Quaternion.identity);
        thePlayer.name = "Player";
        theCamera = (GameObject)Instantiate(cameraPrefab, new Vector3(levels[currentLevel].stairsUp.tileLocation.x, levels[currentLevel].stairsUp.tileLocation.y, -10), Quaternion.identity);
        theCamera.name = "Main Camera";
        theManager.RegisterPlayer(thePlayer, theCamera);
        theCamera.GetComponent<CameraControl>().RegisterPlayer(thePlayer);
    }

    public void SpawnPlayer(bool down) // Arguement denotes the stairs to come out at
    {
        if (down)
        {
            thePlayer.transform.position = new Vector2(levels[currentLevel].stairsUp.tileLocation.x, levels[currentLevel].stairsUp.tileLocation.y);
            theCamera.transform.position = new Vector3(levels[currentLevel].stairsUp.tileLocation.x, levels[currentLevel].stairsUp.tileLocation.y, -10);
            Movement.Move(thePlayer.transform, Vector3.zero, Vector3.zero);
        }
        else
        {
            thePlayer.transform.position = new Vector2(levels[currentLevel].stairsDown.tileLocation.x, levels[currentLevel].stairsDown.tileLocation.y);
            theCamera.transform.position = new Vector3(levels[currentLevel].stairsDown.tileLocation.x, levels[currentLevel].stairsDown.tileLocation.y, -10);
            Movement.Move(thePlayer.transform, Vector3.zero, Vector3.zero);
        }
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

    private void WipeMap()
    {
        possibleFeatureLoctions.Clear();
        FillRect(0, 0, mapWidth, mapHeight, Tile.TileType.SolidRock);
    }

    private void GenerateExitsAndEntrance() // TODO: Tidy up with .Where
    {
        bool stairsBuilt = false;

        while(stairsBuilt == false)
        {
            List<Tile> potentialExitsAndEntrances = new List<Tile>(buildingLevel.openFloorSpace);
            buildingLevel.stairsUp = buildingLevel.openFloorSpace[buildingLevel.rnd.Next(0, buildingLevel.openFloorSpace.Count)];

            for (int i = 0; i < potentialExitsAndEntrances.Count; i++)
            {
                buildingLevel.stairsDown = buildingLevel.openFloorSpace[UnityEngine.Random.Range(0, buildingLevel.openFloorSpace.Count)];
                if (buildingLevel.stairsDown.tileLocation.x > buildingLevel.stairsUp.tileLocation.x + 5
                    || buildingLevel.stairsDown.tileLocation.x < buildingLevel.stairsUp.tileLocation.x - 5)
                {
                    if (buildingLevel.stairsDown.tileLocation.y > buildingLevel.stairsUp.tileLocation.y + 5
                        || buildingLevel.stairsDown.tileLocation.y < buildingLevel.stairsUp.tileLocation.y - 5)
                    {
                        buildingLevel.stairsUp.UpdateTileType(Tile.TileType.UpStairs);
                        buildingLevel.stairsDown.UpdateTileType(Tile.TileType.DownStairs);
                        stairsBuilt = true;
                        break;
                    }
                    else
                    {
                        potentialExitsAndEntrances.Remove(buildingLevel.stairsDown);
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
                        buildingLevel.openFloorSpace.Add(tileScript[startingX + x, startingY + y]);
                    }
                }
            }
        }
    }

    private void CreateFirstRoom()
    {
        buildingLevel.width = buildingLevel.rnd.Next(4, 8);
        buildingLevel.height = buildingLevel.rnd.Next(4, 8);
        buildingLevel.startingX = buildingLevel.rnd.Next(0, mapWidth - buildingLevel.width);
        buildingLevel.startingY = buildingLevel.rnd.Next(0, mapHeight - buildingLevel.height);

        ConstructRoom(buildingLevel.startingX, buildingLevel.startingY, buildingLevel.width, buildingLevel.height, Tile.TileType.Floor);
    }

    private void ConstructRoom(int startingX, int startingY, int roomWidth, int roomHeight, Tile.TileType theTileType)
    {
        if (IsBuildSpaceClear(startingX, startingY, roomWidth, roomHeight))
        {
            FillRect(startingX, startingY, roomWidth, roomHeight, theTileType);
            CollectPossiblePaths(startingX, startingY, roomWidth, roomHeight);
        }
    }

    public void CreateNewFeature() // Overflows
    {
        possibleDoor = BuildDirection(SelectRandomSpot());
        if (possibleDoor != null)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
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

    public void CreateNewFeature(Tile theCurrentTile) // Overflows
    {
        possibleDoor = BuildDirection(theCurrentTile);
        if (possibleDoor != null)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
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

        int roomWidth = UnityEngine.Random.Range(4, 8),
            roomHeight = UnityEngine.Random.Range(3, 6),
            tileX = (int)door.transform.localPosition.x,
            tileY = (int)door.transform.localPosition.y,
            widthModifer = UnityEngine.Random.Range(0, roomWidth),
            heightModifer = UnityEngine.Random.Range(0, roomHeight),
            featureNumber = UnityEngine.Random.Range(0, 2);

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
        Tile randomedTile = possibleFeatureLoctions[UnityEngine.Random.Range(0, possibleFeatureLoctions.Count)];

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

    public Tile GetTile(int x, int y) // Overloads
    {
        return tileScript[x,y];
    }

    public Tile GetTile(Vector2 v2) // Overloads
    {
        return tileScript[(int)v2.x, (int)v2.y];
    }

    public Tile GetTile(Vector3 v3) // Overloads
    {
        return tileScript[(int)v3.x, (int)v3.y];
    }

    public Vector2 MonsterLocation(Level lvl)
    {
        List<Tile> potentialLocations = lvl.openFloorSpace.Where(loc => !loc.occupied && loc.walkable && loc.tileType != Tile.TileType.UpStairs).ToList();
        while (potentialLocations.Count > 0)
        {
            Tile maybeHere = potentialLocations[UnityEngine.Random.Range(0, potentialLocations.Count)];
            if (lvl.levelNumber > 0)
            {

                if (maybeHere.tileLocation.x > levels[0].stairsUp.tileLocation.x + 3 || maybeHere.tileLocation.x < levels[0].stairsUp.tileLocation.x - 3)
                {
                    if (maybeHere.tileLocation.y > levels[0].stairsUp.tileLocation.y + 3 || maybeHere.tileLocation.y < levels[0].stairsUp.tileLocation.y - 3)
                        return maybeHere.transform.position;
                    else
                        potentialLocations.Remove(maybeHere);
                }
                else
                    potentialLocations.Remove(maybeHere);
            }
            return maybeHere.transform.position;
        }
        throw new Exception("can't find a free location");
    }
}