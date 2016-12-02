using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	
	private GameObject player1, ai;
	private GameObject meshPlayer, meshAi;
	private bool isAttacking ;
	private bool stop;
	public GameObject marker;
	public Animator animator;

	void OnGUI () {
		if (ChangeCharacter.isGameStarted) {
			if (Vector3.Distance (meshPlayer.transform.position, meshAi.transform.position) > 40.0f) {
				isAttacking = false;
				animator.SetBool ("Walk Forward", true);
			} else {
				isAttacking = true;
				animator.SetBool ("Walk Forward", false);
				if (!stop) {
					StartCoroutine (Wait());
				}
			}
		} else {
			animator.SetBool ("Walk Forward", false);
			animator.SetBool ("PunchTrigger", false);
		}
	}

	IEnumerator Wait(){
		stop = true;
		yield return new WaitForSeconds (1.0f);
		animator.SetTrigger ("PunchTrigger");
		stop = false;
	}

	// Update is called once per frame
	void Update () {
		if (ChangeCharacter.isGameStarted) {
			if (GameObject.Find ("LiuKang") != null) {
				player1 = GameObject.Find ("LiuKang");
			} else {
				player1 = GameObject.Find ("Scorpion");
			}

			if (GameObject.Find ("Sonya") != null) {
				ai = GameObject.Find ("Sonya");
			} else {
				ai = GameObject.Find ("SubZero");
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
			ai.transform.LookAt (meshPlayer.transform);
		}
	}

	void OnTriggerEnter(Collider col){
		if (ChangeCharacter.isGameStarted) {
			if (col.gameObject.transform.parent.parent.name == "LiuKang" || col.gameObject.transform.parent.parent.name == "Scorpion") {
				if (isAttacking) {
					ChangeCharacter.hp1-=4;
				}
			}
		}
	}

}
