using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScoreScript : MonoBehaviour {
	public TMP_Text thisText;
    // Start is called before the first frame update
    void Start() {
	    thisText.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
    }

}
