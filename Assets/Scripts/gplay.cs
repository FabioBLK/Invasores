using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class gplay : MonoBehaviour {

public Text texto;

	// Use this for initialization
	void Start () {
	
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			//.EnableSavedGames()
				// registers a callback to handle game invitations received while the game is not running.
				//.WithInvitationDelegate(<callback method>)
				// registers a callback for turn based match notifications received while the
				// game is not running.
				//.WithMatchDelegate(<callback method>)
				.Build();
		
		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Social.localUser.Authenticate((bool success) => {
			texto.color = Color.yellow;
		});
	
	}
}
