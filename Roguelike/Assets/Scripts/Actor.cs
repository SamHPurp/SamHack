using UnityEngine;
using System.Collections;

[System.Serializable]
public class Actor : ScriptableObject
{
    public GameObject myGO;

    public virtual Transform location
    {
        get
        {
            return myGO.transform;
        }
    }

    [System.NonSerialized]
    public Generation mapGenerator;

    public bool canMoveDiagonally = true;
    public bool canMove = true;
    public bool paralysed = false;

    [System.NonSerialized]
    public ActorStats myStats;

    protected virtual void Awake()
    {
        myGO = GameObject.CreatePrimitive(PrimitiveType.Quad);
        AddNormalComponents();
        myGO.AddComponent<ActorController>();
    }

    void Start()
    {
        Register();
        mapGenerator = FindObjectOfType<Generation>();
    }

    protected virtual void Register()
    {
        GameFlow.RegisterAsActor(this);
    }

    public virtual void TakeTurn()
    {
    }

    protected virtual void AddNormalComponents()
    {
        //MeshFilter mf = myGO.AddComponent<MeshFilter>();
    }
}