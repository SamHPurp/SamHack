using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    Generation theGenerator;
    GameObject thePlayer;
    GameObject theCamera;

	void Awake()
    {
        if (playerPrefab == null)
        {
            playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
        }
        if (cameraPrefab == null)
        {
            cameraPrefab = (GameObject)Resources.Load("Prefabs/Main Camera");
        }

        theGenerator = FindObjectOfType<Generation>();
        theGenerator.theManager = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Semicolon))
        {
            //theGenerator.ConstructMap(playerPrefab, cameraPrefab);

            theGenerator.BuildAllMaps(playerPrefab, cameraPrefab);
        }
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