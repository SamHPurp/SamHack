using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActorController : MonoBehaviour
{
    public Monster monsterController;

    public Transform location
    {
        get
        {
            return this.transform;
        }
    }
}