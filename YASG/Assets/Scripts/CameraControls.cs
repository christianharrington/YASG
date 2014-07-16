using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector3.right * 10 * Time.deltaTime, Space.World);
	}
}
