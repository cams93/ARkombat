using UnityEngine;
using System.Collections;

public class p2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == "Scorpion" || col.gameObject.name == "LiuKang"){
			print ("eeeee");
			ChangeCharacter.hp1 -= 10;
		}

	}

}
