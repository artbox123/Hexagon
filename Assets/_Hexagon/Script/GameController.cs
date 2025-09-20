using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GameController : MonoBehaviour {

	public float speed = 500.0f;
	public GameObject player;

	private Vector3 worldCenter = new Vector3(0, 0, 0);  //Rotation pivot point

	void OnMouseDrag()
	{
		var test = Input.GetAxisRaw ("Mouse Y");
		player.transform.RotateAround (worldCenter, Vector3.forward, test * speed * Time.smoothDeltaTime);
	}
}