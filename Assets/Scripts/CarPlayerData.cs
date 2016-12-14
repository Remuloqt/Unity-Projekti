using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CarPlayerData : NetworkBehaviour {

    [SyncVar(hook = "UpdatePlayerNameText")]
    public string playerName = "PlayerName";

    public bool gameWon = false;

    [SyncVar(hook = "UpdateScoreText")]
    public int playerScore = 0;

    public Text textPlayerScore;
    public Text textPlayerName;

    void Start()
    {
        if (!isLocalPlayer) return;
        GameObject startAndEndScreenManager = GameObject.Find("StartAndEndScreenManager");
        StartAndEndScreenManagerScript startAndEndScreenManagerScript = startAndEndScreenManager.GetComponent<StartAndEndScreenManagerScript>();
        playerName = startAndEndScreenManagerScript.playerName;
    }

    void UpdatePlayerNameText(string playerName)
    {
        textPlayerName.text = playerName;
    }

    void UpdateScoreText(int score)
    {
        textPlayerScore.text = "" + score;
        CarPlayerData carPlayerData = transform.gameObject.GetComponent<CarPlayerData>();
        carPlayerData.playerScore = score;
    }

    public void OnLapFinished(int score)
    {
        playerScore += score;
    }

    public void OnPickupPickedUp(int score)
    {
        playerScore += score;
    }

}
