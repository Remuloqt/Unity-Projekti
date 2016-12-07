using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenScript : MonoBehaviour {

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

    public void OnButtonMainMenuClick()
    {
        // TODO: what happens?
    }

    public void OnButtonNewGameClick()
    {
        // TODO: what happens?
    }
}
