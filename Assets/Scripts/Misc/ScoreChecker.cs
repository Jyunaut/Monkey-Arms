using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreChecker : MonoBehaviour {
	public TMP_Text scoreText;


    // Update is called once per frame
    void Start() {
	    PlayerPrefs.SetInt("Score", 0);
    }
    
    
    
    void Update() {
	    scoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
    }
}
