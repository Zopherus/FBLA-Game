using UnityEngine;
using System.Collections;

public class CoinCountUpdater : MonoBehaviour {

    private UnityEngine.UI.Text _coinCountDisplay;
    private GameLogicMgr _gameLogicMgr;

	// Use this for initialization
	void Start () {
        _coinCountDisplay = gameObject.GetComponent<UnityEngine.UI.Text>();
        _gameLogicMgr = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<GameLogicMgr>();
	}
	
	// Update is called once per frame
	void Update () {
        _coinCountDisplay.text = _gameLogicMgr.Score.ToString();
	}
}
