using UnityEngine;
using System.Collections;

public class StarBehaviour : MonoBehaviour {
	public GameState GameState;
	public Star Star;

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
