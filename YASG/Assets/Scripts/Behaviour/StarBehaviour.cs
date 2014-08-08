using UnityEngine;
using System.Collections;
using System.Linq;

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
        string starNames = StarSystem.Stars.Select(s => s.Name).Aggregate((a, b) => a + ", " + b);
        Debug.Log("Star system: " + StarSystem.Name + " Behaviour position: " + transform.localPosition + ", model position: " + StarSystem.Coordinates + ". Stars (" + StarSystem.Stars.Count + "): " + starNames);
		renderer.material.color = Color.red;
	}

	public void DeSelect () {
		renderer.material.color = Color.yellow;
	}


	void OnMouseDown () {
		GameState.SelectedStarSystem = this;
	}
}
