using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class ChaseState : MoveState
    {
        // This is subtracted from the units range.
        // Therefore if the other entity keeps on moving this entity can keep attacking it.
        private const float ARRIVE_THRESHOLD = 5.0f;

        private GameEntity _unitToAttack;

        public ChaseState(GameEntity unitToAttack)
            : base(unitToAttack.GetPos())
        {
            _unitToAttack = unitToAttack;
        }

        public override void EnterState(GameEntity unit)
        {
            base.EnterState(unit);
        }

        public override void UpdateState(GameEntity unit)
        {
            base.UpdateState(unit);

            Vector3 unitToAttackPos = _unitToAttack.GetPos();

            // As the unit must move towards a dynamic position.
            unit.MoveTo(unitToAttackPos);

            float attackRange = unit.GetAttackRange();

            // Is the unit within attacking distance of the unit to attack?
            float distance = Vector3.Distance(unitToAttackPos, unit.GetPos());
            if (distance < (attackRange - ARRIVE_THRESHOLD))
            {
                // Start attacking the other object.
                unit.GetStateMachine().ChangeState(new AttackState(_unitToAttack));
            }
        }

        public override void ExitState(GameEntity unit)
        {
            base.ExitState(unit);

            unit.StopMovement();
        }
    }
}
