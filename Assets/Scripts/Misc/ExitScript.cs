using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
	public Button thisButton;
	// Start is called before the first frame update
	void Start() {
		thisButton.onClick.AddListener(TaskOnClick);
	}
    
    
    
	void TaskOnClick() {
		Application.Quit();
	}
}
