using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class AttackState : UnitState<GameEntity>
    {
        private GameEntity _unitToAttack;

        public AttackState(GameEntity unitToAttack)
        {
            _unitToAttack = unitToAttack;
        }

        public void EnterState(GameEntity unit)
        {
            unit.StopMovement();
        }

        public void UpdateState(GameEntity unit)
        {
            Vector3 unitToAttackPos = _unitToAttack.GetPos();

            // Is the unit within attacking distance of the unit to attack?
            float distance = Vector3.Distance(unitToAttackPos, unit.GetPos());

            float attackRange = unit.GetAttackRange();

            if (distance > attackRange)
            {
                unit.GetStateMachine().ChangeState(new ChaseState(unit));
            }
        }



        public void ExitState(GameEntity unit)
        {

        }
    }
}