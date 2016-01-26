using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class MoveState : UnitState<GameEntity>
    {
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
        }

        public virtual void ExitState(GameEntity unit)
        {

        }
    }
}
