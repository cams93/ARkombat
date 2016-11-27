using UnityEngine;
using System.Collections;

public class FighterControl : MonoBehaviour {
	
	public Animator animator;
	private AudioSource source;

	private Transform defaultCamTransform;
	private Vector3 resetPos;
	private Quaternion resetRot;
	private GameObject cam;
	private GameObject fighter, ai;
	public AudioClip hit;
	private GameObject meshPlayer, meshAi;

	private bool isAttacking = false;

	void Start()
	{
		if (GameObject.Find ("Sonya") != null) {
			ai = GameObject.Find ("Sonya");
		} else {
			ai = GameObject.Find ("SubZero");
		}

		if (GameObject.Find ("Scorpion") != null) {
			fighter = GameObject.Find ("Scorpion");
		} else {
			fighter = GameObject.Find ("LiuKang");
		}

		if (GameObject.Find ("Mesh_Berserker") != null) {
			meshPlayer = GameObject.Find ("Mesh_Berserker");
		} else {
			meshPlayer = GameObject.Find ("Mesh_Heavy");
		}

		if (GameObject.Find ("Mesh_Female") != null) {
			meshAi = GameObject.Find ("Mesh_Female");
		} else {
			meshAi = GameObject.Find ("Mesh_Male");
		}

		source = GetComponent<AudioSource>();
		cam = GameObject.FindWithTag("MainCamera");
		defaultCamTransform = cam.transform;
		resetPos = defaultCamTransform.position;
		resetRot = defaultCamTransform.rotation;
		fighter.transform.position = new Vector3(0,0,0);
	}

	void Update(){
		if (ChangeCharacter.isGameStarted) {
			if (GameObject.Find ("Sonya") != null) {
				ai = GameObject.Find ("Sonya");
			} else {
				ai = GameObject.Find ("SubZero");
			}

			if (GameObject.Find ("Scorpion") != null) {
				fighter = GameObject.Find ("Scorpion");
			} else {
				fighter = GameObject.Find ("LiuKang");
			}

			if (GameObject.Find ("Mesh_Berserker") != null) {
				meshPlayer = GameObject.Find ("Mesh_Berserker");
			} else {
				meshPlayer = GameObject.Find ("Mesh_Heavy");
			}

			if (GameObject.Find ("Mesh_Female") != null) {
				meshAi = GameObject.Find ("Mesh_Female");
			} else {
				meshAi = GameObject.Find ("Mesh_Male");
			}
		}
	}

	void OnGUI () {
		int w = Screen.width;
		int h = Screen.height;
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = 30;
		if (ChangeCharacter.isGameStarted) {

			if (GUI.RepeatButton (new Rect (20, w / 3 - 20, w / 4 - 20, h / 8), "Walk Forward", customButton)) {
				if (Vector3.Distance (meshPlayer.transform.position, meshAi.transform.position) > 40.0f) {
					isAttacking = false;
					animator.SetBool ("Walk Forward", true);
				} else {
					animator.SetBool ("Walk Forward", false);
				}
			} else {
				animator.SetBool ("Walk Forward", false);
			}

			if (GUI.RepeatButton (new Rect (20, w / 3 + h / 8, w / 4 - 20, h / 8), "Walk Backward", customButton)) {
				isAttacking = false;
				animator.SetBool ("Walk Backward", true);
			} else {
				animator.SetBool ("Walk Backward", false);
			}

			if (GUI.Button (new Rect (w - w / 4 - 10, h - h / 8 - 20, w / 4, h / 8), "Punch", customButton)) {
				if (!ChangeCharacter.stopEffects)
					source.PlayOneShot (hit, 10);
				isAttacking = true;
				animator.SetTrigger ("PunchTrigger");
			}
		} else {
			animator.SetBool ("Walk Forward", false);
			animator.SetBool ("Walk Backward", false);
			animator.SetBool ("PunchTrigger", false);
		}

	}

	void OnTriggerEnter(Collider col){
		if (ChangeCharacter.isGameStarted) {
			if (col.gameObject.transform.parent.parent.name == "Sonya" || col.gameObject.transform.parent.parent.name == "SubZero") {
				//print("TRIGGER player");
				if (isAttacking) {
					//Decrease AIs life
					//print("TRIGGER player, decrease life");
					ChangeCharacter.hp2--;
				}
			}
		}
	}

}