using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnLevelSelected(int level)
    {
        GameLogicMgr.LevelDifficulty = level;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Test2");
    }

    public void OnHowToPlay()
    {

    }

    public void OnGameQuit()
    {
        Application.Quit();
    }
}
