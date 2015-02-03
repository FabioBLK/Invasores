using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
private bool playerTrue=true;
private float timeLeft;
private float x;
	public void LoadLevel(string name) {
		print ("New level loaded: " + name);
		Application.LoadLevel (name);
		
	}
	
	public void QuitLevel(string name) {
		Debug.Log ("Quit game request");
		Application.Quit();
	}

	public void LoadNextLevel(){
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	void Update(){
		timeLeft += Time.deltaTime;
		if (timeLeft>(x+3.0f)&&!playerTrue){
				LoadNextLevel ();
			}
	}
	
	public void PlayerDestroyed (){
		playerTrue=false;
		x = timeLeft;
	}

}
