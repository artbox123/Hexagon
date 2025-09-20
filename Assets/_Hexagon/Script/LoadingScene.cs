using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField] private Text loading;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();

		StartCoroutine (LoadingText ());
		StartCoroutine (LoadSceneAsync());

		if (!PlayerPrefs.HasKey ("ads")) {
			PlayerPrefs.SetInt ("ads", 1);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("sound")) {
			PlayerPrefs.SetInt ("sound", 1);
			PlayerPrefs.Save ();
		}

		if (!PlayerPrefs.HasKey ("highSec")) {
			PlayerPrefs.SetInt ("highSec", 0);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("highMili")) {
			PlayerPrefs.SetInt ("highMili", 0);
			PlayerPrefs.Save ();
		}
		if (!PlayerPrefs.HasKey ("ShowHint")) {
			PlayerPrefs.SetInt ("ShowHint", 0);
			PlayerPrefs.Save ();
		}
	}
	public IEnumerator LoadingText ()
	{
		float timer = 0;
		while (timer <= 4) {
			timer += 1;

			if (timer == 1)
				loading.text = "Loading";
			if (timer == 2)
				loading.text = "Loading.";
			if (timer == 3)
				loading.text = "Loading..";
			if (timer == 4) {
				loading.text = "Loading...";
				timer = 0;
			}
			yield return new WaitForSeconds (0.3f);
		}
	}

	IEnumerator LoadSceneAsync ()
	{
		yield return new WaitForSeconds (2f); 
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}