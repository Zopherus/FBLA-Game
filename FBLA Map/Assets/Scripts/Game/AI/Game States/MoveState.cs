using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class MoveState : UnitState<GameEntity>
    {
        private const float ARRIVE_TOLERANCE = 0.5f;

        private Vector3 _dest;

        public MoveState(Vector3 dest)
        {
            _dest = dest;
        }

        public virtual void EnterState(GameEntity unit)
        {
            // Just move the unit towards the desired position.
            unit.MoveTo(_dest);
        }

        public virtual void UpdateState(GameEntity unit)
        {
            if (Vector3.Distance(unit.GetPos(), _dest) < ARRIVE_TOLERANCE)
            {
                unit.GetStateMachine().ChangeState(new RestState());
            }
        }

        public virtual void ExitState(GameEntity unit)
        {

        }
    }
}
