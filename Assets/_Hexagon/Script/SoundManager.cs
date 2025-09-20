using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager _Instance;

	public AudioSource Sounds;

	public AudioClip ButtonClick;
	public AudioClip Fail;

	// Use this for initialization
	void Start () {
		if (_Instance) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			_Instance = this;
		}
	}
}