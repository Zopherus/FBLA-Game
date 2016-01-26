using UnityEngine;
using System.Collections.Generic;
using System;
using FBLA.Game.AI;


public class GameLogicMgr : MonoBehaviour
{
    private const string GAME_OBJ_KEY = "GameMgr";
    private const string SCORE_KEY = "sc";
    private const string WIN_STATUS = "wn";
    private const string LVL_DIFF = "ld";

    private static Dictionary<string, object> _persistantInfo = new Dictionary<string, object>();

    private int _score;
    private float _totalTimeOnGame = 0.0f;
    private AIPlayer _aiPlayer;

    public GameObject[] AIDefenderSpawns;

    public static Dictionary<string, object> PersistantInfo
    {
        get
        {
            return _persistantInfo;
        }
    }

    public static bool? WinStatus
    {
        get
        {
            if (_persistantInfo.ContainsKey(WIN_STATUS))
                return (Boolean)_persistantInfo[WIN_STATUS];
            return null;
        }
    }

    public static int LevelDifficulty
    {
        set
        {
            _persistantInfo[LVL_DIFF] = value;
        }
        get
        {
            if (_persistantInfo.ContainsKey(LVL_DIFF))
                return (Int32)_persistantInfo[LVL_DIFF];
            // The default is easy.
            return 0;
        }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public void ResetPersistantInfo()
    {
        _persistantInfo.Clear();
    }

    public void Start()
    {
        _score = 0;
        _aiPlayer = new AIPlayer();
        GameObject spawnerGameObj = GameObject.FindGameObjectWithTag("Spawner");
        Vector3[] aiSpawnLocs = new Vector3[AIDefenderSpawns.Length]; 
        for (int i = 0; i < AIDefenderSpawns.Length; ++i)
        {
            aiSpawnLocs[i] = AIDefenderSpawns[i].transform.position;
        }
        _aiPlayer.Init(LevelDifficulty, aiSpawnLocs, spawnerGameObj.GetComponent<AddObject>());

        if (_persistantInfo.ContainsKey(SCORE_KEY))
            _score = (Int32)_persistantInfo[SCORE_KEY];
    }

    public void Update()
    {
        if (_totalTimeOnGame != float.MaxValue)
        {
            _totalTimeOnGame += Time.deltaTime;
            if (_totalTimeOnGame < 0.0f)
                _totalTimeOnGame = float.MaxValue;
        }

        _aiPlayer.Update();
    }

    public static GameLogicMgr GetInstance()
    {
        return GameObject.FindGameObjectWithTag(GAME_OBJ_KEY).GetComponent<GameLogicMgr>();
    }

    private void OnGameEnded()
    {
        int totalSeconds = (int)_totalTimeOnGame;

        _score += (totalSeconds / 30);

        _persistantInfo[SCORE_KEY] = _score;
    }

    public void GameOver()
    {
        OnGameEnded();

        _persistantInfo[WIN_STATUS] = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void GameWon()
    {
        OnGameEnded();

        _persistantInfo[WIN_STATUS] = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}


