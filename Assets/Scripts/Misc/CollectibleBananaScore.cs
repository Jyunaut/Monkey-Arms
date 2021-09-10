using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBananaScore : MonoBehaviour
{
	
	void OnTriggerEnter2D(Collider2D other) {
	    if (other.CompareTag("Monkey Body")) {
		    int currentScore = PlayerPrefs.GetInt("Score", 0);
		    PlayerPrefs.SetInt("Score", currentScore+1);
			GetComponent<PlayRandomAudio>().PlayRandomSound();
		    Invoke("DisableObj", 1f);
			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;
	    }
    }

	private void DisableObj()
	{
		gameObject.SetActive(false);
	}
    
}
