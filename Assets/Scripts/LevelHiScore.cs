using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelHiScore : MonoBehaviour {

	public Text texto;

	// Use this for initialization
	void Start () {
		
		int textHiScore = 0;
		//x = ScoreControl.FindObjectOfType<ScoreControl>().GetScore();
		//textHiScore = ScoreControl.hiScore;
		textHiScore = PlayerPrefs.GetInt("Score");
		
		print (textHiScore);
		texto.text = ("Hi Score: " + textHiScore.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
