using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
	public float rotateSpeed = 10f;

	void Start()
	{
		InvokeRepeating(nameof(setCameraRotation), 0f, Random.Range(5, 10));
	}
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);
	}
	private void setCameraRotation()
	{
		rotateSpeed = Random.Range(-GameScene.cameraRotationSpeed, GameScene.cameraRotationSpeed);
	}
}