using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D other) {
	    if (other.CompareTag("Monkey Body")) {
		    PlayerPrefs.SetString("winLoseCondition", "lose");
		    SceneManager.LoadScene("Scenes/EndScene");
	    }
    }
}
