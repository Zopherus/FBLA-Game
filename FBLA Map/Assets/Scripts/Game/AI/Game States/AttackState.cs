using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class AttackState : UnitState<GameEntity>
    {
        private GameEntity _unitToAttack;
        private bool _shouldChase;

        public AttackState(GameEntity unitToAttack)
            : this(unitToAttack, true)
        {
        }

        public AttackState(GameEntity unitToAttack, bool shouldChase)
        {
            _unitToAttack = unitToAttack;
            _shouldChase = shouldChase;
        }

        public void EnterState(GameEntity unit)
        {
            unit.StopMovement();
            Debug.Log("Entering attack state");
        }

        public void UpdateState(GameEntity unit)
        {
            if (_unitToAttack == null || _unitToAttack.IsDead())
            {
                unit.GetStateMachine().ChangeState(new RestState());
                return;
            }

            Vector3 unitToAttackPos = _unitToAttack.GetPos();

            // Is the unit within attacking distance of the unit to attack?
            float distance = Vector3.Distance(unitToAttackPos, unit.GetPos());

            float attackRange = unit.GetAttackRange();

            if (distance > attackRange)
            {
                if (_shouldChase)
                    unit.GetStateMachine().ChangeState(new ChaseState(unit));
                else
                    unit.GetStateMachine().ChangeState(new RestState());
                return;
            }

            unit.DamageEnemy(_unitToAttack);
        }



        public void ExitState(GameEntity unit)
        {

        }
    }
}