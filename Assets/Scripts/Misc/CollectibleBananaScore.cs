using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBananaScore : MonoBehaviour
{
	
	void OnTriggerEnter2D(Collider2D other) {
	    if (other.CompareTag("Player")) {
		    int currentScore = PlayerPrefs.GetInt("Score", 0);
		    PlayerPrefs.SetInt("Score", currentScore+1);
	    }
    }
    
    
}
