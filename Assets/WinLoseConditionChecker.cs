using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseConditionChecker : MonoBehaviour {
	public TMP_Text thisText;

	private String winlose;
    // Start is called before the first frame update
    void Start() {
	    winlose = PlayerPrefs.GetString("winLoseCondition", null);
	    if (winlose != null) {
		    if (winlose == "win") {
			    thisText.text = "You Win!";
		    }
		    else {
			    thisText.text = "You Lose!";
		    }
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
