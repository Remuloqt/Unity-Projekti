﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartAndEndScreenManagerScript : MonoBehaviour {

    public InputField inputFieldServerIP;
    public InputField inputFieldServerPort;
    public InputField inputfieldPlayerName;

    public GameObject startingMenu;
    public GameObject endingMenu;

    private bool playerIsHost = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void HideStartingMenu()
    {
        startingMenu.SetActive(false);
    }

    private void ShowStartingMenu()
    {
        startingMenu.SetActive(true);
    }

    private void StartClienting()
    {
        playerIsHost = false;

        NetworkManager networkManager = GetComponent<NetworkManager>();
        networkManager.serverBindAddress = inputFieldServerIP.text;
        networkManager.networkAddress = inputFieldServerIP.text;
        networkManager.networkPort = int.Parse(inputFieldServerPort.text);
        networkManager.StartClient();
    }

    public void OnButtonConnectClick()
    {
        HideStartingMenu();
        StartClienting();
    }

    private void StartHosting()
    {
        playerIsHost = true;

        NetworkManager networkManager = GetComponent<NetworkManager>();
        networkManager.serverBindAddress = inputFieldServerIP.text;
        networkManager.networkAddress = inputFieldServerIP.text;
        networkManager.networkPort = int.Parse(inputFieldServerPort.text);
        networkManager.StartHost();
    }

    public void OnButtonCreateClick()
    {
        HideStartingMenu();
        StartHosting();
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
    }

    public void EndGame()
    {
        DisconnectGame();
        ShowEndingMenu();
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
    }

    private void ShowEndingMenu()
    {
        endingMenu.SetActive(true);
    }

}
