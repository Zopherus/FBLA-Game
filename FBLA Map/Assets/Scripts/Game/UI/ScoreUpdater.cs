using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUpdater : MonoBehaviour {

    private Text _scoreDispTxt;
    private GameLogicMgr _gameLogicMgr;

	// Use this for initialization
	void Start ()
    {
        _scoreDispTxt = gameObject.GetComponent<Text>();
        _gameLogicMgr = GameLogicMgr.GetInstance();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _scoreDispTxt.text = _gameLogicMgr.Score.ToString();
    }
}
