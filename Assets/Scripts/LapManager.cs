using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LapManager : NetworkBehaviour {

    // Possible way to implement giving player points for completing a lap
    //private PlayerScore playerScoreScript;

    private bool newLap = false;
    public int lapsFinished = 0;

    private TextMesh lapTextNumber;

	// Use this for initialization
	void Start () {

        if (!isLocalPlayer) return;

        // Possible way to get the playerScoreScript for the rest of the code
        GameObject carPlayerObject = this.gameObject;
        //playerScoreScript = carPlayerObject.GetComponent<PlayerScoreScript>();

        GameObject mapObject = GameObject.Find("GameMap");
        GameObject lapStuffObject = mapObject.transform.Find("LapStuff").gameObject;
        GameObject lapTextNumberPositionObject = lapStuffObject.transform.Find("LapsDrivenNumberTextPosition").gameObject;

        GameObject lapTextNumberObject = new GameObject("LapsDrivenNumberText");
        lapTextNumberObject.transform.parent = lapStuffObject.transform;
        lapTextNumberObject.transform.position = lapTextNumberPositionObject.transform.position;
        lapTextNumberObject.transform.rotation = lapTextNumberPositionObject.transform.rotation;

        lapTextNumberObject.AddComponent<MeshRenderer>();
        lapTextNumberObject.AddComponent<TextMesh>();
        lapTextNumber = lapTextNumberObject.GetComponent<TextMesh>();

        lapTextNumber.fontSize = 40;
        lapTextNumber.text = ""+0;

        Instantiate(lapTextNumberObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LapEndPassed()
    {
        if (!isLocalPlayer || !newLap) return;

        newLap = false;
        // Possible way to inform of new lap done
        //playerScoreScript.OnLapFinished();

        ++lapsFinished;
        lapTextNumber.text = "" + lapsFinished;
    }

    public void LapStartPassed()
    {
        if (!isLocalPlayer || newLap) return;

        newLap = true;
    }
}
