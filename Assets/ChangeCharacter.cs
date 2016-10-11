using UnityEngine;
using System.Collections;

public class ChangeCharacter : MonoBehaviour {
	public GameObject samurai, berserker, female, male;
	private bool isGameStarted = false;
	private float timeLeft = 90.0f;


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
		if(isGameStarted){
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0){
				//Application.LoadLevel ("GameOver");
			}
		}
	}

	void OnGUI () {
		int w = Screen.width;
		int h = Screen.height;
		if(w<h){
			h /= 2;
		}


		if (!isGameStarted) {
			GUIStyle customBox = new GUIStyle ("box");
			customBox.fontSize = 20;

			GUIStyle customButton = new GUIStyle ("button");
			customButton.fontSize = 30;

			GUI.Box (new Rect (10, 10, w / 4, h / 4 + 70), "Player 1", customBox);
			GUI.Box (new Rect (w - w / 4 - 10, 10, w / 4, h / 4 + 70), "Player 2", customBox);


			if (GUI.Button (new Rect (20, 40, w / 4 - 20, h / 8), "Red samurai", customButton)) {
				samurai.active = true;
				berserker.active = false;
			}
			if (GUI.Button (new Rect (20, h / 8 + 50, w / 4 - 20, h / 8), "Berserker", customButton)) {
				samurai.active = false;
				berserker.active = true;
			}


			if (GUI.Button (new Rect (w - w / 4, 40, w / 4 - 20, h / 8), "Female", customButton)) {
				female.active = true;
				male.active = false;
			}
			if (GUI.Button (new Rect (w - w / 4, h / 8 + 50, w / 4 - 20, h / 8), "Male", customButton)) {
				female.active = false;
				male.active = true;
			}


			if (GUI.Button (new Rect (w / 2 - w / 8 - 10, h / 2 - 20, w / 4 - 20, h / 8), "Start game", customButton)) {
				if (samurai.activeSelf) {
					//print ("Samurai activo");
					DontDestroyOnLoad (samurai);
					Destroy (berserker);
				} else {
					//print ("Berserker activo");
					DontDestroyOnLoad (berserker);
					Destroy (samurai);
				}
				if (female.activeSelf) {
					//print ("Female activo");
					DontDestroyOnLoad (female);
					Destroy (male);
				} else {
					//print ("Male activo");
					DontDestroyOnLoad (male);
					Destroy (female);
				}
				isGameStarted = true;

				//Application.LoadLevel ("AR");
			}
		} else {
			GUIStyle customLabel = new GUIStyle ("label");
			customLabel.fontSize = 30;

			GUI.Label (new Rect (w / 2 - 100, 10, 200, 100), timeLeft+"", customLabel);
		}

	}

}
