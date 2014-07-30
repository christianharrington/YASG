using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	private const float KeyboardSpeed = 0.02f;
	private const float DragSpeed = 1f;
	private const int ScrollArea = 25;
	private const float ScrollSpeed = 1f;
	private const int ScrollWheelSpeed = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 translation = new Vector3();
		
		translation -= new Vector3(-Input.GetAxis("Horizontal") * camera.orthographicSize * KeyboardSpeed, 
		                           -Input.GetAxis("Vertical") * camera.orthographicSize * KeyboardSpeed, 
		                           0);

		if (Input.GetKey(KeyCode.Plus)) {
			camera.orthographicSize -= 0.1f;
		}
		if (Input.GetKey(KeyCode.Minus)) {
			camera.orthographicSize += 0.1f;
		}

		camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * ScrollWheelSpeed;

		// Move camera with mouse
		if (Input.GetMouseButton(2)) // MMB
		{
			// Hold button and drag camera around
			translation -= new Vector3(Input.GetAxis("Mouse X") * camera.orthographicSize * DragSpeed * Time.deltaTime,
			                           Input.GetAxis("Mouse Y") * camera.orthographicSize * DragSpeed * Time.deltaTime, 0);
		}
		else
		{
			// Move camera if mouse pointer reaches screen borders
			if (Input.mousePosition.x < ScrollArea)
			{
				translation += Vector3.right * camera.orthographicSize * -ScrollSpeed * Time.deltaTime;
			}
			
			if (Input.mousePosition.x >= Screen.width - ScrollArea)
			{
				translation += Vector3.right * camera.orthographicSize * ScrollSpeed * Time.deltaTime;
			}
			
			if (Input.mousePosition.y < ScrollArea)
			{
				translation += Vector3.up * camera.orthographicSize * -ScrollSpeed * Time.deltaTime;
			}
			
			if (Input.mousePosition.y > Screen.height - ScrollArea)
			{
				translation += Vector3.up * camera.orthographicSize * ScrollSpeed * Time.deltaTime;
			}
		}

		transform.Translate(translation);
	}
}
