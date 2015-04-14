using UnityEngine;
using System.Collections;

[System.Serializable]
public class Actor : ScriptableObject
{
    [System.NonSerialized]
    public GameObject myGO;

    public virtual Transform location
    {
        get
        {
            return myGO.transform;
        }
    }

    public virtual Point locationPoint
    {
        get
        {
            return new Point((int)myGO.transform.position.x, (int)myGO.transform.position.y);
        }
    }

    [System.NonSerialized]
    public Generation mapGenerator;

    public bool canMoveDiagonally = true;
    public bool canMove = true;
    public bool paralysed = false;

    [System.NonSerialized]
    protected Renderer myRenderer;

    [System.NonSerialized]
    public ActorStats myStats;

    protected virtual void Awake()
    {
        myGO = GameObject.CreatePrimitive(PrimitiveType.Quad);
        myRenderer = myGO.GetComponent<Renderer>();
        myStats = new ActorStats(this);
        Register();
        mapGenerator = FindObjectOfType<Generation>();
    }

    protected virtual void Register()
    {
        GameFlow.RegisterAsActor(this);
    }

    public virtual void TakeTurn()
    {
        Vector3 direction = mapGenerator.directions[Random.Range(0, mapGenerator.directions.Length)];
        Movement.Move(location, direction, -Vector3.zero);
    }
}