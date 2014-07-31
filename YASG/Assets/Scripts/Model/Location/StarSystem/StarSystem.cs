using UnityEngine;
using System.Collections.Generic;

public class StarSystem : MonoBehaviour, ILocation {
	public GameState GameState { get; set; }

	public ILocation Parent { get; set; }
	public HashSet<ILocation> Sublocations { get; set; }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Select () {
		renderer.material.color = Color.red;
	}

	public void DeSelect () {
		renderer.material.color = Color.yellow;
	}


	void OnMouseDown () {
		GameState.SetStar(this);
	}
}
