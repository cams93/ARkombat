using UnityEngine;
using System.Collections;

public class FighterControl : MonoBehaviour {
	
	public Animator animator;
	private AudioSource source;

	private Transform defaultCamTransform;
	private Vector3 resetPos;
	private Quaternion resetRot;
	private GameObject cam;
	private GameObject fighter;
	public AudioClip hit;

	void Start()
	{
		source = GetComponent<AudioSource>();
		cam = GameObject.FindWithTag("MainCamera");
		defaultCamTransform = cam.transform;
		resetPos = defaultCamTransform.position;
		resetRot = defaultCamTransform.rotation;
		fighter = GameObject.FindWithTag("Player");
		fighter.transform.position = new Vector3(0,0,0);
	}

	void OnGUI () 
	{
		int w = Screen.width;
		int h = Screen.height;
		GUIStyle customButton = new GUIStyle ("button");
		customButton.fontSize = 30;
		if (ChangeCharacter.isGameStarted) {

			if (GUI.RepeatButton (new Rect (20, w/3 -20, w / 4 - 20, h / 8), "Walk Forward", customButton)) 
			{
				animator.SetBool("Walk Forward", true);
			}
			else
			{
				animator.SetBool("Walk Forward", false);
			}

			if (GUI.RepeatButton (new Rect (20, w/3 + h / 8, w / 4 - 20, h / 8), "Walk Backward", customButton)) 
			{
				animator.SetBool("Walk Backward", true);
			}
			else
			{
				animator.SetBool("Walk Backward", false);
			}

			if (GUI.Button (new Rect (w - w/4 - 10, h - h / 8 - 20, w / 4, h / 8), "Punch", customButton)) 
			{
				if(!ChangeCharacter.stopEffects) source.PlayOneShot(hit,10);
				animator.SetTrigger("PunchTrigger");
			}
		}
	}
}