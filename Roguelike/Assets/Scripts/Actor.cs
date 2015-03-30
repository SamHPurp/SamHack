using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    public Transform location;
    public bool canMoveDiagonally = true; //fix later
    public bool canMove = true;
    public bool paralysed = false;
}