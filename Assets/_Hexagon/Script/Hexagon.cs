using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour {

	public Rigidbody2D rb2D;
	private int[] rotationList = new int[6]{ 30, 90, 150, 210, 270, 330 };

	// Use this for initialization
	void Start () {
		transform.Rotate (0, 0, rotationList [Random.Range (0, 6)]);
		transform.localScale = Vector3.one * 7f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale -= Vector3.one * GameScene.shrinkSpeed * Time.deltaTime;

		if (transform.localScale.x <= .05f) {
			Destroy (gameObject);
		}
	}
}