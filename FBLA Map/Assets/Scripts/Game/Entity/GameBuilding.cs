using UnityEngine;
using System.Collections;
using System;

public class GameBuilding : GamePlayer {
    public override void MoveTo(Vector3 position)
    {
        // Buildings can't move!
    }

    public override void StopMovement()
    {
        // Buildings can't move!
    }

    protected override bool ShouldRenderHealthBar()
    {
        return true;
    }
}
