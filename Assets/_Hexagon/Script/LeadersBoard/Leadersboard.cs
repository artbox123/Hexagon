using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class Leadersboard : MonoBehaviour {

    private bool isLogin = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		Invoke ("InitAndSignin", 1f);
	}

	void InitAndSignin()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			// Create client configuration
			PlayGamesClientConfiguration config = new 
				PlayGamesClientConfiguration.Builder ()
				.Build ();

			// Enable debugging output (recommended)
			PlayGamesPlatform.DebugLogEnabled = true;

			// Initialize and activate the platform
			PlayGamesPlatform.InitializeInstance (config);
			PlayGamesPlatform.Activate ();
			// END THE CODE TO PASTE INTO START
			Signin ();
		}
	}
	private void Signin()
	{
		Debug.Log ("===== Signin");
		if (Social.localUser.authenticated) {
			return;
		}

		Social.localUser.Authenticate ((bool success) => {
			// handle success or failure
			if (success) {
				Debug.Log ("==== gpg login succes");
                isLogin = true;
			} else {
                isLogin = false;
				Debug.Log ("==== gpg login failed");
			}
		});
	}

	public void ReportTime(long time)
	{
		Social.ReportScore (time, "CgkIs9D06ZcIEAIQAQ", (bool success) => {
			// handle success or failure
			if (success) {
				Debug.Log ("==== time reporting succes");
			} else {
				Debug.Log ("==== time reporting failed");
			}
		});
	}

	public void ShowLeadersboard()
	{
        if (isLogin) {
            Social.ShowLeaderboardUI();
        }
        else {
            Signin();
        }
	}
}