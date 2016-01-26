using UnityEngine;
using System.Collections;

namespace FBLA.Game.AI
{
    public class DyingState : UnitState<GameEntity>
    {
        public DyingState()
        {
        }

        public virtual void EnterState(GameEntity unit)
        {
            if (unit is GameBuilding)
            {
                // Someone just lost.
                OnBuildingDestroyed(unit as GameBuilding);
            }
            else if (unit is GameUnit)
            {
            }
        }

        private void OnUnitKilled(GameUnit gameUnit)
        {
            if (gameUnit.GetTeam() != 0)
            {
                // Increase the player's score.
                GameLogicMgr gameLogicMgr = GameLogicMgr.GetInstance();
                gameLogicMgr.Score += 1;
            }
        }

        private void OnBuildingDestroyed(GameBuilding destroyedBuilding)
        {
            GameLogicMgr gameLogicMgr = GameLogicMgr.GetInstance();
            if (destroyedBuilding.GetTeam() != 0)
            {
                // Display you won message.
                gameLogicMgr.GameWon();
            }
            else
            {
                // Display the you lost message.
                gameLogicMgr.GameOver();
            }
        }

        public virtual void UpdateState(GameEntity unit)
        {
        }

        public virtual void ExitState(GameEntity unit)
        {

        }
    }
}