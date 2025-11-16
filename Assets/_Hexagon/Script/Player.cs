using UnityEngine;

public class Player : MonoBehaviour {
	
	private GameScene gameScene;
		
	void Start()
	{
		gameScene = FindAnyObjectByType<GameScene> ();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name.StartsWith ("Collider")) {
			gameScene.GameOverDlg.SetActive (true);
			Time.timeScale = 0;
			SoundManager._Instance.Sounds.PlayOneShot (SoundManager._Instance.Fail);

			AdsManager.Instance.ShowInterstitial();
		}
	}
}