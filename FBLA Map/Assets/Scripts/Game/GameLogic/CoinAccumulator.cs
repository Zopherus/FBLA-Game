using UnityEngine;
using System.Collections;

namespace FBLA.Game.GameLogic
{
    public class CoinAccumulator
    {
        private int _coinPerSec;
        private int _totalCoin;
        private float _timeSinceLastUpdate;

        public int TotalCoin
        {
            get { return _totalCoin; }
        }

        public CoinAccumulator(int coinPerSec)
            : this(coinPerSec, 0)
        {

        }

        public CoinAccumulator(int coinPerSec, int initialCoin)
        {
            _coinPerSec = coinPerSec;
            _totalCoin = initialCoin;
            _timeSinceLastUpdate = 0.0f;
        }

        public void Subtract(int coin)
        {
            _totalCoin -= coin;
        }

        public void Update(float elapsedTime)
        {
            _timeSinceLastUpdate += elapsedTime;

            if (_timeSinceLastUpdate > 1.0f)
            {
                _totalCoin += _coinPerSec;
                _timeSinceLastUpdate = 0.0f;
            }
        }
    }
}
