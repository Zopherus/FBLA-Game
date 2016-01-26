using UnityEngine;
using System.Collections.Generic;
using FBLA.Game.GameLogic;


namespace FBLA.Game.AI
{
    public class AIPlayer
    {
        private const int WAVE_COUNT = 4;
        private const string SPAWN_UNIT_ASSET = "TankPrefab";

        private CoinAccumulator _coinAc;
        private GameBuilding _enemyBase;
        private List<GameEntity> _defenders = new List<GameEntity>();
        private List<GameEntity> _wave = new List<GameEntity>();
        private Vector3[] _defenderLocations;
        private AddObject _spawner;

        private GameEntity SpawnUnit()
        {
            GameObject spawnedObj = _spawner.SpawnObject(SPAWN_UNIT_ASSET, 1);
            //spawnedObj.transform.position = location;
            GameEntity spawnedEntity = spawnedObj.GetComponent<GameEntity>();
            return spawnedEntity;
        }

        public void Update()
        {
            _coinAc.Update(Time.deltaTime);

            // This AI is grealty simplified. 
            // It prioritizes building defender units.
            // Then it builds waves and sends them to attack.

            for (int i = 0; i < _defenders.Count; ++i)
            {
                if (_defenders[i].IsDead())
                    _defenders.RemoveAt(i);
            }

            for (int i = 0; i < _wave.Count; ++i)
            {
                if (_wave[i].IsDead())
                    _wave.RemoveAt(i);
            }


            if (_coinAc.TotalCoin > 10.0f && _defenders.Count < _defenderLocations.Length)
            {
                GameEntity defender = SpawnUnit();
                Vector3 seekLocation = _defenderLocations[_defenders.Count];

                defender.GetStateMachine().ChangeState(new MoveState(seekLocation));
                _defenders.Add(defender);
                _coinAc.Subtract(10);
            }

            if (_coinAc.TotalCoin > 10.0f && _wave.Count < WAVE_COUNT)
            {
                GameEntity waveUnit = SpawnUnit();
                _wave.Add(waveUnit);
                _coinAc.Subtract(10);
            }

            if (_wave.Count == WAVE_COUNT)
            {
                // Move all of the units out to the player's base.
                foreach (GameEntity waveUnit in _wave)
                {
                    waveUnit.GetStateMachine().ChangeState(new ChaseState(_enemyBase));
                }
            }
        }

        public void Init(int difficulty, Vector3[] defenderLocations, AddObject spawner)
        {
            // The player base is the first base.
            _enemyBase = spawner.SpawnBases[0].GetComponent<GameBuilding>();
            _defenderLocations = defenderLocations;
            _spawner = spawner;
            switch (difficulty)
            {
                case 0:
                    _coinAc = new CoinAccumulator(10);
                    break;
                case 1:
                    _coinAc = new CoinAccumulator(15);
                    break;
                default:
                    _coinAc = new CoinAccumulator(20);
                    break;
            }


        }
    }
}
    