using UnityEngine;
using System.Collections;

public class Player : Actor
{
    PlayerInputHandler mover;

    protected override void Awake()
    {
        myGO = GameObject.CreatePrimitive(PrimitiveType.Quad);
        AddNormalComponents();
        mover = myGO.AddComponent<PlayerInputHandler>();
        mover.playerController = this;
        myStats = new ActorStats(this);
    }

    public override Transform location
    {
        get
        {
            return null;
        }
    }

    protected override void Register() // Stops player register on the DungeonMaster
    {
    }
}