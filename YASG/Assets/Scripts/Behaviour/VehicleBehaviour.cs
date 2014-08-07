using UnityEngine;
using System.Collections;

public class VehicleBehaviour : MonoBehaviour {
	public GameState GameState;
	public IVehicle Vehicle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = Vehicle.Coordinates;
	    transform.localPosition = new Vector3(pos.x, 0, pos.y);
	}
	
	public void Select () {
        Debug.Log("Behaviour position: " + transform.localPosition + ", model position: " + Vehicle.Coordinates);
		renderer.material.color = Color.cyan;
	}
	
	public void DeSelect () {
		renderer.material.color = Color.white;
	}
	
	
	void OnMouseDown () {
		GameState.SelectedVehicle = this;
	}
}
