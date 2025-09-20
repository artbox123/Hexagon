	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameScene : MonoBehaviour {

	public GameObject GameOverDlg;
	public GameObject PauseDlg;
	public GameObject InformationDlg;
	public GameObject HintPanel;
	public GameObject StartGame;
	public GameObject counter;

	public static float spawnRate = 0.5f;
	public static float shrinkSpeed = 0.8f;
	public static int shapLimit = 6;
	public static int cameraRotationSpeed = 10;

	void Start()
	{
		if (PlayerPrefs.GetInt ("ShowHint") == 0) {
			HintPanel.SetActive (true);
			StartCoroutine (setHintPanel ());
		} else {
			Time.timeScale = 1;
			StartGame.GetComponent <Spawner> ().enabled = true;
			counter.GetComponent <Counter> ().enabled = true;
		}

		if (PlayerPrefs.GetInt ("sound") == 1) {
			AudioListener.volume = 1;	
		} else {
			AudioListener.volume = 0;
		}
	}

	//set hint panel 
	IEnumerator setHintPanel()
	{
		yield return new WaitForSeconds (2f);
		HintPanel.GetComponent <Button> ().enabled = true;
	}
	public void DisableHintPanel()
	{
		HintPanel.SetActive (false);
		PlayerPrefs.SetInt ("ShowHint", 1);
		PlayerPrefs.Save ();
		StartGame.GetComponent <Spawner> ().enabled = true;
		counter.GetComponent <Counter> ().enabled = true;
	}

	public void Pause()
	{
		Time.timeScale = 0;
		PauseDlg.SetActive (true);
	}
	public void PlaySound(){
		SoundManager._Instance.Sounds.PlayOneShot (SoundManager._Instance.ButtonClick);
	}
	void Update()
	{
		if (Input.GetKeyUp (KeyCode.Escape) && !HintPanel.activeSelf) {
			if (InformationDlg.activeSelf) {
				InformationDlg.SetActive (false);
			} else if (PauseDlg.activeSelf) {
				PauseDlg.SetActive (false);
				Time.timeScale = 1;
			} else if (GameOverDlg.activeSelf) {				
				SceneManager.LoadScene ("Main");
			} else {
				PauseDlg.SetActive (true);
				Time.timeScale = 0;

				AdsManager.Instance.ShowInterstitial();
			}				
		}
	}

	public void ContinueGame()
	{
		FindObjectOfType<Spawner>().ContinueGame();
		GameOverDlg.SetActive(false);
		Time.timeScale = 1;
		StartGame.GetComponent<Spawner>().enabled = true;
		counter.GetComponent<Counter>().enabled = true;
		counter.GetComponent<Counter>().StartCounter();
	}
}