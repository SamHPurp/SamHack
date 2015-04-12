using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Canvas mainMenu;
    GameObject monsterPrefab;
    Generation theGenerator;
    GameObject thePlayer;
    GameObject theCamera;
    public Player playerControl;

	void Awake()
    {
        DungeonMaster.Register(this);

        if (monsterPrefab == null)
        {
            monsterPrefab = (GameObject)Resources.Load("Prefabs/Monster");
        }

        theGenerator = FindObjectOfType<Generation>();
        theGenerator.theManager = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Semicolon))
        {
            theGenerator.BuildAllMaps();
        }
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

    public void RegisterPlayer(GameObject player, GameObject theBuiltCamera)
    {
        thePlayer = player;
        theCamera = theBuiltCamera;
    }

    public void BuildPlayer()
    {
        playerControl = (Player)ScriptableObject.CreateInstance("Player");
        playerControl.myGO.name = "Player";
        theCamera = Camera.main.gameObject;
        RegisterPlayer(playerControl.myGO, theCamera);
        theCamera.GetComponent<CameraControl>().RegisterPlayer(playerControl.myGO);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }

    public void StartLevel()
    {
        theGenerator.BuildAllMaps();
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