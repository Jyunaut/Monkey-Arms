using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockScript : MonoBehaviour {
	private float timer;
	private bool timeStart = false;

	public TMP_Text thisText;
	
    // Start is called before the first frame update
    void Start() {
	    timeStart = true;
    }

    // Update is called once per frame
    void Update() {
	    if (timeStart) {
		    timer += Time.deltaTime;
	    }
	    String minutes = Mathf.Floor(timer / 60).ToString("00");
	    String seconds = Mathf.Floor(timer % 60).ToString("00");

	    thisText.text = string.Format("{0} : {1}", minutes, seconds);

    }
}
