using UnityEngine;
using System.Collections.Generic;


namespace FBLA.Game.AI
{
    public class RestState : UnitState<GameEntity>
    {

        public RestState()
        {
        }

        public virtual void EnterState(GameEntity unit)
        {

        }

        public virtual void UpdateState(GameEntity unit)
        {
            // Check if there any units nearby if so attack the unit.
            List<GameEntity> nearbyUnits = EntityMgr.GetAllEntities((GameEntity compareEntity) =>
            {
                float distance = Vector3.Distance(compareEntity.GetPos(), unit.GetPos());

                return (distance < unit.GetAttackRange()) && (compareEntity.GetTeam() != unit.GetTeam());
            });

            if (nearbyUnits.Count > 0)
            {
                Debug.Log("Attacking on rest!");
                // For now just attack the first unit.
                GameEntity entityToAttack = nearbyUnits[0];
                unit.GetStateMachine().ChangeState(new AttackState(entityToAttack, false));
            }
        }

        public virtual void ExitState(GameEntity unit)
        {

        }
    }
}