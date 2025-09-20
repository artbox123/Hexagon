using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

	public static CoinManager _Instance;
	public static int Coins;

	void Awake()
	{		
		_Instance = this;
	}

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("coins")) {
			Coins = 2000;
			PlayerPrefs.SetInt ("coins", 2000);
			PlayerPrefs.Save ();
		} else {
			Coins = PlayerPrefs.GetInt ("coins");
		}
	}

	public void addCoins(int coin)
	{
		Coins += coin;
		PlayerPrefs.SetInt ("coins", Coins);
		PlayerPrefs.Save ();
	}

	public void minusCoins(int coin)
	{
		Coins -= coin;
		PlayerPrefs.SetInt ("coins", Coins);
		PlayerPrefs.Save ();
	}
}