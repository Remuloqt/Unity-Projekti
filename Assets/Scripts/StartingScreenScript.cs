using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartingScreenScript : MonoBehaviour {

    public InputField inputFieldServerIP;
    public InputField inputFieldServerPort;
    public InputField inputfieldPlayerName;

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
        var startingMenu = GameObject.Find("StartingMenu");
        startingMenu.SetActive(false);
    }

    public void OnButtonConnectClick()
    {
        HideStartingMenu();

        NetworkManager networkManager = GetComponent<NetworkManager>();
        networkManager.serverBindAddress = inputFieldServerIP.text;
        networkManager.networkAddress = inputFieldServerIP.text;
        networkManager.networkPort = int.Parse(inputFieldServerPort.text);
        networkManager.StartClient();
    }

    public void OnButtonCreateClick()
    {
        HideStartingMenu();

        NetworkManager networkManager = GetComponent<NetworkManager>();
        networkManager.serverBindAddress = inputFieldServerIP.text;
        networkManager.networkAddress = inputFieldServerIP.text;
        networkManager.networkPort = int.Parse(inputFieldServerPort.text);
        networkManager.StartHost();
    }
}
