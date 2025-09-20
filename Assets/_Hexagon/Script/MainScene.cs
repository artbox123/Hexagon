using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainScene : MonoBehaviour {

	public GameObject QuitDialog;
	public GameObject SoundOff;
	public GameObject VideoConfirmation;
	public GameObject InformationDlg;

	public Button Ads;
	public Text Coins;
    
	void Start()
	{
		GameScene.spawnRate = 0.5f;
		GameScene.shrinkSpeed = 0.8f;
		GameScene.shapLimit = 6;
		Time.timeScale = 1;
		CheckAdStatus ();

		if (PlayerPrefs.GetInt ("sound") == 1) {
			AudioListener.volume = 1;
			SoundOff.SetActive (false);		
		} else {
			AudioListener.volume = 0;
			SoundOff.SetActive (true);
		}
		Coins.text = CoinManager.Coins.ToString ();
	}
	public void Play()
	{
		if (CoinManager.Coins >= 100) {
			SceneManager.LoadScene ("Game");
			CoinManager._Instance.minusCoins (100);
		} else {
			InformationDlg.SetActive (true);
			InformationDlg.transform.Find ("Message").GetComponent <Text> ().text = "You require minimum 100 coins to play a game";
		}
	}

	public void Yes()
	{
		Application.Quit ();
	}

	public void PlaySound()
	{
		SoundManager._Instance.Sounds.PlayOneShot (SoundManager._Instance.ButtonClick);
	}
	void Update()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (QuitDialog.activeSelf) {
				QuitDialog.SetActive (false);
			} else if (VideoConfirmation.activeSelf) {
				VideoConfirmation.SetActive (false);
			} else if (InformationDlg.activeSelf) {
				InformationDlg.SetActive (false);
			}  else {
				QuitDialog.SetActive (true);
				//fullScreenAd.ShowInterstitial ();
			}
		}
	}

	public void SoundOnOff()
	{
		if (SoundOff.activeSelf) {
			SoundOff.SetActive (false);
			PlayerPrefs.SetInt ("sound", 1);
			PlayerPrefs.Save ();
			AudioListener.volume = 1;
		} else {
			SoundOff.SetActive (true);
			PlayerPrefs.SetInt ("sound", 0);
			PlayerPrefs.Save ();
			AudioListener.volume = 0;
		}
	}

	public void RemoveAd()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			//GameObject.FindObjectOfType<UnityPurchase> ().BuyNonConsumable ();
		} else {
			InformationDlg.SetActive (true);
			InformationDlg.transform.Find ("Message").GetComponent <Text> ().text = "No internet connection";
		}
	}

	public void Leaderboards()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			GameObject.FindObjectOfType<Leadersboard> ().ShowLeadersboard ();
		} else {
			InformationDlg.SetActive (true);
			InformationDlg.transform.Find ("Message").GetComponent <Text> ().text = "No internet connection";
		}
	}

	public void CheckAdStatus()
	{
		if (PlayerPrefs.GetInt ("ads") == 0) {
			Ads.interactable = false;
		}
	}

	public void WatchVideo()
	{
        AdsManager.Instance.ShowRewardVideo();
	}

	private bool CheckWatchVideoCount()
	{
		if (!PlayerPrefs.HasKey ("VideoCount")) {
			PlayerPrefs.SetInt ("VideoCount", 0);
			PlayerPrefs.Save ();
			return true;
		} else {
			int count = PlayerPrefs.GetInt ("VideoCount");
			if (PlayerPrefs.HasKey ("WatchVideoDay")) {
				DateTime PrevDT = Convert.ToDateTime (PlayerPrefs.GetString ("WatchVideoDay"));
				DateTime NowDT = DateTime.Now;

				if (PrevDT.Day == NowDT.Day) {
					if (count < 5) {
						//Debug.Log ("=== u allow to  watch : "+count);
						//VideoCount.text = "You watched " + count + " video ad out of 3";
						return true;
					} else {
						//Debug.Log ("=== u reached watch video limit : "+count);
						//WatchBtn.interactable = false;
						//VideoCount.text = "Maximum 3 video allowed in a day";
						return false;
					}
				} else {
					//Debug.Log ("=== day change refresh counter");
					PlayerPrefs.SetInt ("VideoCount", 0);
					return true;
				}
			} else {
				//Debug.Log ("=== u not watch video yet");
				return true;
			}
		}
	}

	public void SaveVideoDay()
	{
		int count = PlayerPrefs.GetInt ("VideoCount");
		count++;
		PlayerPrefs.SetInt ("VideoCount",count);
		PlayerPrefs.SetString ("WatchVideoDay", DateTime.Now.ToString ());
		PlayerPrefs.Save ();
	}

	public void Install()
	{
		Application.OpenURL ("https://play.google.com/store/apps/developer?id=Artbox+Infotech");
	}
	public void PrivacyPolicy()
	{
		Application.OpenURL ("https://www.freeprivacypolicy.com/privacy/view/972d33c12c611db56647e25f123187cf");
	}
}