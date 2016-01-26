using UnityEngine;
using System.Collections;
using System;

namespace FBLA.Game.AI
{
    public class StateMachine<T>
    {
        private T _owner;
        private UnitState<T> _currentState;
        private UnitState<T> _prevState;

        public StateMachine(T owner)
        {
            _owner = owner;
            _currentState = null;
            _prevState = null;
        }

        public void Update()
        {
            if (_currentState != null)
                _currentState.UpdateState(_owner);
        }

        public void ChangeState(UnitState<T> changeState)
        {
            _prevState = _currentState;

            _currentState.ExitState(_owner);

            _currentState = changeState;
            _currentState.EnterState(_owner);
        }

        public void RevertToPrev()
        {
            ChangeState(_prevState);
        }

        public bool IsInState(Type stateType)
        {
            return _currentState.GetType() == stateType;
        }
    }
}