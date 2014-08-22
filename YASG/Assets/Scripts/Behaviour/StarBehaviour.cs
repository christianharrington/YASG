using UnityEngine;
using System.Collections;
using System.Linq;

public class StarBehaviour : MonoBehaviour {
	public GameState GameState;
	public StarSystem StarSystem;

    private const float defaultIntensity = 0.2f;
    private const float selectedIntensity = 1.5f;

    private Color defaultColor = Color.yellow;
    private Color selectedColor = Color.red;

    private Color defaultMaterialColor = Color.yellow;
    private Color playerMaterialColor = Color.blue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameState.Player.Location is StarSystem && StarSystem == GameState.Player.Location as StarSystem) {
            renderer.material.color = playerMaterialColor;
            renderer.light.intensity = selectedIntensity;
        }
        else if (renderer.material.color == playerMaterialColor) {
            renderer.material.color = defaultMaterialColor;
            renderer.light.intensity = defaultIntensity;
        }
	}

	public void Select () {
        string starNames = StarSystem.Stars.Select(s => s.Name).Aggregate((a, b) => a + ", " + b);
        Debug.Log("Star system: " + StarSystem.Name + " Behaviour position: " + transform.localPosition + ", model position: " + StarSystem.Coordinates + ". Stars (" + StarSystem.Stars.Count + "): " + starNames);
        renderer.light.intensity = selectedIntensity;
        renderer.light.color = selectedColor;
	}

	public void DeSelect () {
        renderer.light.intensity = defaultIntensity;
        renderer.light.color = defaultColor;
	}


	void OnMouseDown () {
		GameState.SelectedStarSystem = this;
	}
}
