using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class ChaseState : MoveState
    {
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

            // Is the unit within attacking distance of the unit to attack?
            float distance = Vector3.Distance(unitToAttackPos, unit.GetPos());
            Debug.Log(distance);
        }

        public override void ExitState(GameEntity unit)
        {
            base.ExitState(unit);
        }
    }
}
