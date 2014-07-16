using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	private float speed = 0.02f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.Translate(Input.GetAxis("Horizontal") * camera.orthographicSize * speed, 
		                    Input.GetAxis("Vertical") * camera.orthographicSize * speed, 
		                    0);

		if (Input.GetKey(KeyCode.Plus)) {
			camera.orthographicSize -= 0.1f;
		}
		if (Input.GetKey(KeyCode.Minus)) {
			camera.orthographicSize += 0.1f;
		}
	}
}
