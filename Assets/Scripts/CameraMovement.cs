using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;
	public bool manualCamControl = true;
	public	float f=0.0f;

	// Use this for initialization
	void Start () {

		offset = transform.position - player.transform.position;

	}
	
	// Update is called once per frame
	void LateUpdate () {

			transform.position = player.transform.position + offset;
	}

}
