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
	
	}
	
	public void Select () {
		renderer.material.color = Color.cyan;
	}
	
	public void DeSelect () {
		renderer.material.color = Color.white;
	}
	
	
	void OnMouseDown () {
		GameState.SetVehicle(this);
	}
}
