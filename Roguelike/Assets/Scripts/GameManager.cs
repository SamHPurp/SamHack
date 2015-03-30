using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    GameObject playerPrefab;
    GameObject cameraPrefab;
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
            theGenerator.GenerateTheRooms(30);
            theGenerator.SpawnPlayer(playerPrefab, cameraPrefab);
            theCamera.GetComponent<CameraControl>().RegisterPlayer(thePlayer);
        }
    }

    public void RegisterPlayer(GameObject player, GameObject theBuiltCamera)
    {
        thePlayer = player;
        theCamera = theBuiltCamera;
    }
}