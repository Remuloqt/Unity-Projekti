using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartAndEndScreenManagerScript : MonoBehaviour {

    public InputField inputFieldServerIP;
    public InputField inputFieldServerPort;
    public InputField inputfieldPlayerName;
    private string serverIP;
    private int serverPort;

    public string playerName;

    public GameObject startingMenu;
    public GameObject endingMenu;
    public Text endingMenuScoreText;
    public Text endingMenuWinLossText;

    public AudioClip startingMenuMusic;
    public AudioClip gameMusic;
    public AudioClip endingVictoryMusic;
    public AudioClip endingLoserMusic;

    private AudioSource audioSource;

    private bool playerIsHost = false;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        // Forgive me
        Application.targetFrameRate = 60;

        ShowStartingMenu();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonQuitClick()
    {
        Application.Quit();
    }

    private void HideStartingMenu()
    {
        startingMenu.SetActive(false);
        audioSource.Stop();
    }

    private void ShowStartingMenu()
    {
        startingMenu.SetActive(true);
        audioSource.clip = startingMenuMusic;
        audioSource.Play();
    }

    private bool CheckUIDataValidity()
    {
        bool allGood = false;
        try
        {
            serverIP = inputFieldServerIP.text;

            bool serverPortResult = false;
            serverPortResult = int.TryParse(inputFieldServerPort.text, out serverPort);
            playerName = inputfieldPlayerName.text;
            allGood = serverPortResult;
        }
        catch
        {
            allGood = false;
        }
        return allGood;
    }

    private NetworkManager CreateNetworkManagerFromUIData()
    {
        NetworkManager networkManager = GetComponent<NetworkManager>();
        networkManager.serverBindAddress = serverIP;
        networkManager.networkAddress = serverIP;
        networkManager.networkPort = serverPort;
        return networkManager;
    }

    private void StartClienting()
    {
        playerIsHost = false;

        NetworkManager networkManager = CreateNetworkManagerFromUIData();
        networkManager.StartClient();

        StartGameMusic();
    }

    public void OnButtonConnectClick()
    {
        if (CheckUIDataValidity() == false) return;

        HideStartingMenu();
        StartClienting();
    }

    private void StartHosting()
    {
        playerIsHost = true;

        NetworkManager networkManager = CreateNetworkManagerFromUIData();
        networkManager.StartHost();

        StartGameMusic();
    }

    public void OnButtonCreateClick()
    {
        if (CheckUIDataValidity() == false) return;

        HideStartingMenu();
        StartHosting();
    }

    private void StartGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    private void StopGameMusic()
    {
        audioSource.Stop();
    }

    private void DisconnectGame()
    {
        if (!playerIsHost)
        {
            NetworkManager networkManager = GetComponent<NetworkManager>();
            networkManager.StopClient();
        }
        else
        {
            NetworkManager networkManager = GetComponent<NetworkManager>();
            networkManager.StopHost();
        }

        StopGameMusic();
    }

    public void EndGame(CarPlayerData data)
    {
        DisconnectGame();
        ShowEndingMenu(data);
    }

    public void OnButtonMainMenuClick()
    {
        DisconnectGame();
        HideEndingMenu();
        ShowStartingMenu();
    }

    public void OnButtonNewGameClick()
    {
        DisconnectGame();
        HideEndingMenu();
        StartClienting();
    }

    private void HideEndingMenu()
    {
        endingMenu.SetActive(false);
        audioSource.Stop();
    }

    private void ShowEndingMenu(CarPlayerData data)
    {
        endingMenu.SetActive(true);

        Debug.Log("Game ended. Player won = " + data.gameWon);

        endingMenuScoreText.text = "" + data.playerScore;

        audioSource.clip = data.gameWon ? endingVictoryMusic : endingLoserMusic;
        endingMenuWinLossText.text = data.gameWon ? "won!" : "lost!";
        audioSource.Play();
    }

}
