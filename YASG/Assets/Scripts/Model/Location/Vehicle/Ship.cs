using UnityEngine;
using System.Collections.Generic;
using System;

public class Ship : MonoBehaviour, ITurnable, IVehicle {
	public GameState GameState;
	public StarSystem Destination;
	public ILocation Parent { get; set; }
	public HashSet<ILocation> Sublocations { get; set; }

	public float Speed = 1.0f;

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
		GameState.SetShip(this);
	}

	public void Turn (TimeSpan turnTime, DateTime targetDate) {
		float years = (float) (turnTime.TotalDays / 365);

		Vector3 movement = Vector3.MoveTowards(transform.localPosition, Destination.transform.localPosition, Speed * years);

		if (Destination != null && Destination.transform.localPosition != transform.localPosition) {
			iTween.MoveTo (gameObject, movement, 1.0f);
		}
	}
}
