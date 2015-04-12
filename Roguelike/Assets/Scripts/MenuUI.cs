using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuUI : MonoBehaviour
{
    GameManager theManager;
    Generation theGenerator;
    public Canvas mainMenu;

    void Awake()
    {
        theManager = FindObjectOfType<GameManager>();
        theGenerator = FindObjectOfType<Generation>();
    }

    public void StartLevel()
    {
        theGenerator.BuildAllMaps(theManager.playerPrefab, theManager.cameraPrefab);
        mainMenu.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}