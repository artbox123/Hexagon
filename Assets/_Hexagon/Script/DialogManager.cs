using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour {
	
	public Text Sec;
	public Text Coins;
	public Button ContinueBtn;

	GameScene gameScene;

	void Awake()
	{
		gameScene = GameObject.FindObjectOfType<GameScene> ();
	}
	void OnEnable()
	{
		if (name == "GameOverDialog") {
			Counter timer = GameObject.FindObjectOfType<Counter> ();
			timer.StopCounter ();

			Sec.text = timer.secondsText.text + " : " + timer.miliSecondsText.text;
			Coins.text = CoinManager.Coins.ToString ();

			Leadersboard board = GameObject.FindObjectOfType<Leadersboard> ();
			if (board != null) {
				board.ReportTime (int.Parse (timer.secondsText.text));
			}

			if (CoinManager.Coins >= 100) {
				ContinueBtn.interactable = true;
			} else {
				ContinueBtn.interactable = false;
			}
		}
	}

	public void PlaySound(){
		SoundManager._Instance.Sounds.PlayOneShot (SoundManager._Instance.ButtonClick);
	}

	public void Home()
	{
		SceneManager.LoadScene ("Main");
	}
	public void Restart()
	{
		SceneManager.LoadScene ("Game");
	}
	public void Continue()
	{
		Time.timeScale = 1;
	}

	public void WatchVedio()
	{
        AdsManager.Instance.ShowRewardVideo();
	}
	public void ContinueWithCoins()
	{
		if (CoinManager.Coins >= 100) {			
			CoinManager._Instance.minusCoins (100);
			FindObjectOfType<GameScene>().ContinueGame();
		}
	}
}