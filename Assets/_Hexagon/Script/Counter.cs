using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

	public Text secondsText;
	public Text miliSecondsText;

	public Text bestSecText;
	public Text bestMiliSecText;

	private int seconds;
	private int miliSeconds;

	private delegate void CounterDelegate();
	private CounterDelegate counterDelegate;

	private float timer;
	private int speedMaker;
	private int shapLimit;

	private int bestSec;
	private int bestMili;

	void Start()
	{
		speedMaker = 60;
		shapLimit = 50;
		secondsText.text = "00";
		miliSecondsText.text = "00";
		ResetCounter ();
		StartCounter ();

		bestSec = PlayerPrefs.GetInt ("highSec");
		bestMili = PlayerPrefs.GetInt ("highMili");

		bestSecText.text = bestSec.ToString ();
		bestMiliSecText.text = bestMili.ToString ();
	}

	void Update () {
		if (counterDelegate != null)
			counterDelegate();
	}

	public void StartCounter()
	{
		counterDelegate -= Count;
		counterDelegate += Count;	
		InvokeRepeating ("CheckTime", 0.5f, 1f);
	}

	public void StopCounter()
	{
		counterDelegate -= Count;
	}

	public void ResetCounter()
	{
		timer = 0;
	}

	private void Count()
	{
		if (Time.timeScale == 1) {
			timer += (1f / 60f);

			seconds = Mathf.FloorToInt (timer);
			miliSeconds = Mathf.FloorToInt ((timer - seconds) * 60);

			secondsText.text = seconds < 10 ? "0" + seconds.ToString () : seconds.ToString ();
			miliSecondsText.text = miliSeconds < 10 ? "0" + miliSeconds.ToString () : miliSeconds.ToString ();

			setHighScore (int.Parse (secondsText.text), int.Parse (miliSecondsText.text));
		}
	}

	private void CheckTime()
	{
		if (seconds == speedMaker) {
			speedMaker += 30;	
			GameScene.spawnRate += 0.1f;
			GameScene.shrinkSpeed += 0.1f;
			if (GameScene.cameraRotationSpeed >= 30) {
				GameScene.cameraRotationSpeed = 30;
			} else {
				GameScene.cameraRotationSpeed += 10;
			}
		}
		if (seconds == shapLimit && GameScene.shapLimit > 3) {
			shapLimit += 50;	
			GameScene.shapLimit--;
		}
	}

	private void setHighScore(int Sec,int miliSec)
	{
		if (Sec >= bestSec) {
			bestSec = Sec;
			bestMili = miliSec;

			PlayerPrefs.SetInt ("highSec", Sec);
			PlayerPrefs.Save ();

			PlayerPrefs.SetInt ("highMili", miliSec);
			PlayerPrefs.Save ();

			bestSecText.text = Sec.ToString ();
			bestMiliSecText.text = miliSec.ToString ();
		}
	}
}