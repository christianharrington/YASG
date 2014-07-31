using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public GameState GameState;

	public List<IVehicle> Vehicle = new List<IVehicle>();

	public int Credits;

	public StarSystem Location;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
