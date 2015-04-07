using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    GameObject monsterPrefab;
    Generation theGenerator;
    GameObject thePlayer;
    GameObject theCamera;

	void Awake()
    {
        DungeonMaster.Register(this);

        if (playerPrefab == null)
        {
            playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
        }
        if (cameraPrefab == null)
        {
            cameraPrefab = (GameObject)Resources.Load("Prefabs/Main Camera");
        }
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
            theGenerator.BuildAllMaps(playerPrefab, cameraPrefab);
        }
    }

    public Monster TheMonster(Vector2 v2)
    {
        GameObject x = (GameObject)Instantiate(monsterPrefab, v2, Quaternion.identity);
        Monster y = x.GetComponent<Monster>();
        return y;
    }

    public void RemoveMonster(Monster me)
    {
        Destroy(me);
        Debug.Log("Destroying " + me);
    }

    public void RegisterPlayer(GameObject player, GameObject theBuiltCamera)
    {
        thePlayer = player;
        theCamera = theBuiltCamera;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}