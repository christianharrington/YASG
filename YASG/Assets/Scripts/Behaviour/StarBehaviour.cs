using UnityEngine;
using System.Collections;

public class StarBehaviour : MonoBehaviour {
	public GameState GameState;
	public StarSystem StarSystem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Select () {
        Debug.Log("Behaviour position: " + transform.localPosition + ", model position: " + StarSystem.Coordinates);
		renderer.material.color = Color.red;
	}

	public void DeSelect () {
		renderer.material.color = Color.yellow;
	}


	void OnMouseDown () {
		GameState.SelectedStarSystem = this;
	}
}
