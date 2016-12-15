using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CarPlayerData : NetworkBehaviour {

    [SyncVar(hook="UpdatePlayerNameText")]
    public string playerName = "PlayerName";

    public bool gameWon = false;

    [SyncVar(hook = "UpdateScoreText")]
    public int playerScore = 0;

    public Text textPlayerScore;
    public Text textPlayerName;

    void Start()
    {
        UpdatePlayerNameText(playerName);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GameObject startAndEndScreenManager = GameObject.Find("StartAndEndScreenManager");
        StartAndEndScreenManagerScript startAndEndScreenManagerScript = startAndEndScreenManager.GetComponent<StartAndEndScreenManagerScript>();
        string _playerName = startAndEndScreenManagerScript.playerName;
        CmdSetPlayerName(_playerName);
    }

    [Command]
    void CmdSetPlayerName(string name)
    {
        playerName = name;
    }

    void UpdatePlayerNameText(string playerNameInput)
    {
        playerName = playerNameInput;
        textPlayerName.text = playerNameInput;
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
