using UnityEngine;
using System.Collections;

public class p1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == "SubZero" || col.gameObject.name == "Sonya"){
			print ("jkkjdskdskj");
			ChangeCharacter.hp2 -= 10;
		}

	}

}
