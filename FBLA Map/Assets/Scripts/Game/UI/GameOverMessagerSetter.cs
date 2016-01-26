using UnityEngine;
using System.Collections;

public class GameOverMessagerSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var dispTxt = gameObject.GetComponent<UnityEngine.UI.Text>();
        bool? hasWon = GameLogicMgr.WinStatus;
        if (hasWon.HasValue)
            dispTxt.text = hasWon.Value ? "You Win!" : "Game Over";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
