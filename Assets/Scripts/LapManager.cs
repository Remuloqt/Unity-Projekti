using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour {

    // Possible way to implement giving player points for completing a lap
    //private PlayerScore playerScoreScript;

    private bool newLap = false;
    private int lapsFinished = 0;

    private TextMesh lapTextNumber;

	// Use this for initialization
	void Start () {

        // Possible way to get the playerScoreScript for the rest of the code
        GameObject collidersObject = transform.parent.gameObject;
        GameObject carPlayerObject = collidersObject.transform.parent.gameObject;
        //playerScoreScript = carPlayerObject.GetComponent<PlayerScoreScript>();

        GameObject mapObject = GameObject.Find("GameMap");
        GameObject lapStuffObject = mapObject.transform.Find("LapStuff").gameObject;
        GameObject lapTextNumberObject = lapStuffObject.transform.Find("LapsDrivenNumberText").gameObject;
        lapTextNumber = lapTextNumberObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "LapEnd" && newLap)
        {
            newLap = false;

            // Possible way to inform of new lap done
            //playerScoreScript.OnLapFinished();
            ++lapsFinished;
            lapTextNumber.text = "" + lapsFinished;
        }

        if(other.gameObject.name == "LapStart" && !newLap)
        {
            newLap = true;
        }
    }
}
