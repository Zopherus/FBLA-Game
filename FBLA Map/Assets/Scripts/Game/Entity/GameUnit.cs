using UnityEngine;
using System.Collections;
using System;

public class GameUnit : SelectablePlayer
{
    private NavMeshAgent agent;


    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GetSpeed();
    }



    public override void DamageEnemy(GameEntity gameEntity)
    {
        if (!CanAttack())
            return;        
        // Spawn the particle system to show the unit is being show at.
        GameObject shotPS = (GameObject)Instantiate(Resources.Load("ShotPrefab"));
        Vector3 offset = new Vector3(0.0f, 3.0f, 0.0f);
        shotPS.transform.position = this.GetPos() + offset;
        // Orient the particle system such that it is pointing towards the object being shot at.

        base.DamageEnemy(gameEntity);
    }

    protected override bool ShouldRenderHealthBar()
    {
        return Selected || (WasRecentlyAttacked());
    }

    public override void StopMovement()
    {
        if (agent.destination != transform.position)
            agent.destination = transform.position;
    }

    public override void MoveTo(Vector3 position)
    {
        if (agent.destination != position)
            agent.destination = position;
    }
}
