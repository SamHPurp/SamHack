using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Canvas mainMenu;
    GameObject monsterPrefab;
    Generation mapGenerator;
    GameObject thePlayer;
    GameObject theCamera;
    public Player playerControl;
    int cameraDistance = -10;

	void Awake()
    {
        DungeonMaster.Register(this);

        if (monsterPrefab == null)
            monsterPrefab = (GameObject)Resources.Load("Prefabs/Monster");

        mapGenerator = FindObjectOfType<Generation>();
        mapGenerator.theManager = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Semicolon))
            mapGenerator.BuildAllMaps();
    }

    public void RemoveMonster(Actor me)
    {
        GameFlow.UnregisterAsActor(me);
        Destroy(me.myGO);
    }

    public void RemoveMonster(Player me)
    {
        GameFlow.UnregisterAsActor(me);
        Destroy(me);
    }

    public void PlaceMonster(Monster monster, Point spawnLocation)
    {
        monster.myGO.transform.position = new Vector3(spawnLocation.x,spawnLocation.y, -0.3f);
    }

    public void RegisterPlayer(GameObject player, GameObject theBuiltCamera)
    {
        thePlayer = player;
        theCamera = theBuiltCamera; // fix later
    }

    public void BuildPlayer()
    {
        playerControl = (Player)ScriptableObject.CreateInstance("Player");
        playerControl.myGO.name = "Player";
        theCamera = Camera.main.gameObject;
        RegisterPlayer(playerControl.myGO, theCamera);
        theCamera.GetComponent<CameraControl>().RegisterPlayer(playerControl.myGO);
    }

    public void SpawnPlayer(bool down) // Arguement denotes the stairs to come out at
    {
        Level newLevel = mapGenerator.levels[Game.currentLevel];
        if (down)
        {
            playerControl.myGO.transform.position = new Vector2(newLevel.stairsUp.x, newLevel.stairsUp.y);
            Camera.main.transform.position = new Vector3(newLevel.stairsUp.x, newLevel.stairsUp.y, cameraDistance);
            Movement.Move(playerControl.myGO.transform, Vector3.zero, Vector3.zero);
        }
        else
        {
            playerControl.myGO.transform.position = new Vector2(newLevel.stairsDown.x, newLevel.stairsDown.y);
            Camera.main.transform.position = new Vector3(newLevel.stairsDown.x, newLevel.stairsDown.y, cameraDistance);
            Movement.Move(playerControl.myGO.transform, Vector3.zero, Vector3.zero);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }

    public void StartLevel()
    {
        mapGenerator.BuildAllMaps();
        mainMenu.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Load()
    {
        SaveLoad.LoadGame();
        mainMenu.enabled = false;
    }
}