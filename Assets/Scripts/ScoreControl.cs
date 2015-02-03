using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour {
	public Text text;
	public static int score=0;
	public static int hiScore = 0;
	
	// Use this for initialization
	void Start () {
		text.text = ("Score: " + score.ToString());
		ScoreReset ();
	}
	
	public void ScoreAdding(int x){
		score+=x;
		text.text = ("Score: " + score.ToString());
		if (score>hiScore){
			hiScore=score;
		}
		if (hiScore>PlayerPrefs.GetInt ("Score")){
			PlayerPrefs.SetInt("Score",hiScore);
		}
	}
	
	public void ScoreReset(){
		if (score>hiScore){
		hiScore=score;
		}
		score=0;
	}
	
	public int GetScore(){
		return score;
	}

}
