using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlayAgain()
    {
        // The difficulty should already be set from the last play.
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test2");
    }

    public void OnMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
