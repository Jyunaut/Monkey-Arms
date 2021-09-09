using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
	    if (other.CompareTag("Monkey Body")) {
		    PlayerPrefs.SetString("winLoseCondition", "win");
		    SceneManager.LoadScene("Scenes/EndScene");
	    }
    }
}
