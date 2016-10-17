using UnityEngine;
using System.Collections;

public class ChangeCharacter : MonoBehaviour {
	public GameObject liuKang, scorpion, sonya, subzero;
	private bool isGameStarted = false;
	private bool stopBackground = false;
	private bool stopEffects = false;
	private float timeLeft = 90.0f;
	private float hp1 = 100.0f;
	private float hp2 = 100.0f;

	private string music = "stop music";
	private string effects = "stop effects";

	private AudioSource source;

	public AudioClip liu_start;
	public AudioClip sonya_start;
	public AudioClip scorpion_start;
	public AudioClip subzero_start;

	public AudioClip liu_wins;
	public AudioClip sonya_wins;
	public AudioClip scorpion_wins;
	public AudioClip subzero_wins;

	public AudioClip fight;
	public AudioClip fatality;
	public AudioClip finish_him;
	public AudioClip finish_her;
	public AudioClip perfect;

	public AudioClip background;
	public AudioClip arena;

	void Awake () {
		source = GetComponent<AudioSource>();
	}


	// Use this for initialization
	void Start () {
		liuKang = GameObject.Find ("LiuKang");
		scorpion = GameObject.Find ("Scorpion");
		sonya = GameObject.Find ("Sonya");
		subzero = GameObject.Find ("SubZero");

		scorpion.active = false;
		subzero.active = false;
	}

	// Update is called once per frame
	void Update () {
		if(isGameStarted){
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0){
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

			GUI.Box (new Rect (10, w/3, w / 4, h / 4 + 70), "Settings", customBox);

			if (GUI.Button (new Rect (20, w/3 + 30, w / 4 - 20, h / 8), music, customButton)) {
				stopBackground = !stopBackground;
				if (stopBackground) {
					source.Stop ();
				} else {
					source.Play ();
				}
			}
			if (GUI.Button (new Rect (20, w/3 + h / 8 + 50, w / 4 - 20, h / 8), effects, customButton)) {
				stopEffects = !stopEffects;
			}


			if (GUI.Button (new Rect (20, 40, w / 4 - 20, h / 8), "Liu Kang", customButton)) {
				if(!stopEffects) source.PlayOneShot(liu_start,10);
				liuKang.active = true;
				scorpion.active = false;
			}
			if (GUI.Button (new Rect (20, h / 8 + 50, w / 4 - 20, h / 8), "Scorpion", customButton)) {
				if(!stopEffects) source.PlayOneShot(scorpion_start,10);
				liuKang.active = false;
				scorpion.active = true;
			}


			if (GUI.Button (new Rect (w - w / 4, 40, w / 4 - 20, h / 8), "Sonya", customButton)) {
				if(!stopEffects) source.PlayOneShot(sonya_start,10);
				sonya.active = true;
				subzero.active = false;
			}
			if (GUI.Button (new Rect (w - w / 4, h / 8 + 50, w / 4 - 20, h / 8), "Sub-Zero", customButton)) {
				if(!stopEffects) source.PlayOneShot(subzero_start,10);
				sonya.active = false;
				subzero.active = true;
			}


			if (GUI.Button (new Rect (w / 2 - w / 8 - 10, h / 2 - 20, w / 4 - 20, h / 8), "Fight", customButton)) {
				if (liuKang.activeSelf) {
					//print ("Samurai activo");
					DontDestroyOnLoad (liuKang);
					Destroy (scorpion);
				} else {
					//print ("Berserker activo");
					DontDestroyOnLoad (scorpion);
					Destroy (liuKang);
				}
				if (sonya.activeSelf) {
					//print ("Female activo");
					DontDestroyOnLoad (sonya);
					Destroy (subzero);
				} else {
					//print ("Male activo");
					DontDestroyOnLoad (subzero);
					Destroy (sonya);
				}

				isGameStarted = true;
				source.clip = arena;
				if(!stopEffects) source.PlayOneShot(fight,10);
				if(!stopBackground) source.PlayDelayed(fight.length);

				//Application.LoadLevel ("AR");
			}
		} else {
			GUIStyle customLabel = new GUIStyle ("label");
			customLabel.fontSize = 30;

			GUI.Label (new Rect (w / 2 - 100, 10, 200, 100), timeLeft+"", customLabel);
		}

	}

}
