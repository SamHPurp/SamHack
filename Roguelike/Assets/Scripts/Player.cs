using UnityEngine;
using System.Collections;

public class Player : Actor
{
    PlayerInputHandler mover;

    protected override void Awake()
    {
        base.Awake();
        mover = myGO.AddComponent<PlayerInputHandler>();
        mover.playerController = this;
    }

    protected override void Register() // Stops player register on the DungeonMaster
    {
    }

    public override void TakeTurn() // Stops user registering a turn
    {
    }
}