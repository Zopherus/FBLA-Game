﻿using UnityEngine;
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
            Debug.Log("Chasing enemy");
        }

        public override void UpdateState(GameEntity unit)
        {
            base.UpdateState(unit);

            if (_unitToAttack == null || _unitToAttack.IsDead())
            {
                unit.GetStateMachine().ChangeState(new RestState());
                return;
            }

            Vector3 unitToAttackPos = _unitToAttack.GetPos();

            // As the unit must move towards a dynamic position.
            unit.MoveTo(unitToAttackPos);

            float extra = _unitToAttack is GameBuilding ? GameBuilding.EXTRA_ATTACK_DIST : 0.0f;
            float attackRange = unit.GetAttackRange() + extra;

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
            Debug.Log("Stopping chasing enemy");
        }
    }
}
