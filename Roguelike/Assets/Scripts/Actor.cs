using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    public Transform location;
    public Generation mapGenerator;
    public bool canMoveDiagonally = true;
    public bool canMove = true;
    public bool paralysed = false;
    public ActorStats myStats = new ActorStats();

    void Start()
    {
        Register();
        location = this.transform;
        mapGenerator = FindObjectOfType<Generation>();
    }

    protected virtual void Register()
    {
        GameFlow.RegisterAsActor(this);
    }

    public virtual void TakeTurn()
    {

    }
}