using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public GameObject thePlayer;
    public GameManager theManager;

    public void RegisterPlayer(GameObject player)
    {
        thePlayer = player;
        thePlayer.GetComponent<PlayerControl>().theMainCamera = this;
    }
	
    public void UpdateLocation(int x, int y)
    {
        transform.position = new Vector3(x, y, transform.position.z);
    }
}