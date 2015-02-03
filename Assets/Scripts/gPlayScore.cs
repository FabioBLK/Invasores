using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class gPlayScore : MonoBehaviour {

public Text texto;

	// Use this for initialization
	void Start () {
		
		// post score 12345 to leaderboard ID "CgkI5dO0sakTEAIQBw")
		Social.ReportScore(PlayerPrefs.GetInt ("Score"), "CgkI5dO0sakTEAIQBw", (bool success) => {
			texto.color=Color.yellow;
		});
		
		Social.ShowLeaderboardUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
