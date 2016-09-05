using UnityEngine;
using System.Collections;

public class ChangeCharacter : MonoBehaviour {
	public GameObject samurai, berserker, female, male;

	// Use this for initialization
	void Start () {
		samurai = GameObject.Find ("Samurai");
		berserker = GameObject.Find ("Berserker");
		female = GameObject.Find ("Female");
		male = GameObject.Find ("Male");

		berserker.active = false;
		male.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		int w = Screen.width;
		int h = Screen.height;
		if(w<h){
			h /= 2;
		}
		GUIStyle customBox = new GUIStyle("box");
		customBox.fontSize = 20;

		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = 30;

		GUI.Box(new Rect(10, 10, w/4, h/4+70), "Player 1", customBox);
		GUI.Box(new Rect(w-w/4-10, 10, w/4, h/4+70), "Player 2", customBox);

		if(GUI.Button(new Rect(20, 40, w/4-20, h/8), "Red samurai", customButton)) {
			samurai.active = true;
			berserker.active = false;
		}
		if(GUI.Button(new Rect(20, h/8+50, w/4-20, h/8), "Berserker", customButton)) {
			samurai.active = false;
			berserker.active = true;
		}


		if(GUI.Button(new Rect(w-w/4, 40, w/4-20, h/8), "Female", customButton)){
			female.active = true;
			male.active = false;
		}
		if(GUI.Button(new Rect(w-w/4, h/8+50, w/4-20, h/8), "Male", customButton)) {
			female.active = false;
			male.active = true;
		}
	}

}
