using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour
{
    public Transform location
    {
        get
        {
            return this.transform;
        }
    }
}