using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
	public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameStateConditionals() {
	    // if Player hits the pause menu button, switch case to 2
	    GameStateSwitcher(2);
	    
	    // if Player reaches goal, switch case to 3
	    GameStateSwitcher(3);
	    
	    // if Player health is zero or below, switch case to -1
	    GameStateSwitcher(-1);
    }
    
    void GameStateSwitcher(int stateInt) {
	    switch (stateInt) {
		    case 0:
			    //Switch scene to main menu
			    break;
		    
		    case 1:
			    //Switch scene to playable level
			    break;
		    
		    case 2:
			    //Switch scene to pause menu
			    break;
		    
		    case 3:
			    //Switch state to win condition
			    break;
		    
		    case -1:
			    //Switch state to loss condition
			    break;
		    
		    default:
			    //main menu
			    break;
	    }
    }
    
    
    
}
