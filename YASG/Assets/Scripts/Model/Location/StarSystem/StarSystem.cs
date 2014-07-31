using UnityEngine;
using System.Collections;

public class StarSystem : MonoBehaviour {
	public GameState GameState { get; set; }

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
