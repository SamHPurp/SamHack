using UnityEngine;
using System.Collections;

[System.Serializable]
public class Monster : Actor
{
    [System.NonSerialized]
    ActorController mover;

    protected override void Awake()
    {
        base.Awake();
        mover = myGO.AddComponent<ActorController>();
        mover.monsterController = this;

        Material mat = (Material)Resources.Load("Materials/Monster");
        myRenderer.material = new Material(mat);
    }
}