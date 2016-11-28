using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript2 : MonoBehaviour
{
	#region FIELDS
	// The Player's movement speed
	public float speed;

	// The players current health
	private int currentHealth;

	// The player's max health
	public int maxHealth;

	// The healt's transform, this is used for moving the object
	public RectTransform healthTransform;

	// The health text
	public Text healthText;

	// The health's image, this is used for color changing
	public Image visualHealth;

	// The health's y pos
	private float cachedY;

	// The minimum value of the health's x pos
	private float minXValue;

	// The max value of the health's x pos
	private float maxXValue;

	// The current xValue of the health
	private float currentXValue;

	// How often can I take damage
	public float cooldown;

	// Indicates if the we can take damage or not
	private bool onCD;

	
	// The healthbar's canvas
	public Canvas canvas;
	#endregion

	#region PROPERTIES
	// Property for accessing the players health
	public int Health {
		get { return currentHealth; }
		set {
			currentHealth = value;
			HandleHealthbar();
		}
	}
	#endregion

	// Use this for initialization
	void Start() {   
		onCD = false;
		cachedY = healthTransform.position.y; //Caches the healthbar's start pos
		maxXValue = healthTransform.position.x; //The max value of the xPos is the start position
		minXValue = healthTransform.position.x - healthTransform.rect.width*canvas.scaleFactor; //The minValue of the xPos is startPos - the width of the bar
		currentHealth = maxHealth; //Sets the current healt to the maxHealth
	}

	// Update is called once per frame
	void Update() {   
		Health = (int)ChangeCharacter.hp2;
		healthText.text = ChangeCharacter.name2;
		HandleMovement();
	}
	
	// Handels the players movement
	private void HandleMovement() {   
		float translation = speed * Time.deltaTime;
		transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
	}
		
	// Handles the healthbar by moving it and changing color
	private void HandleHealthbar() {   
		healthText.text = "Health: " + currentHealth;
		currentXValue = Map(currentHealth, 0, maxHealth, minXValue, maxXValue);
		healthTransform.position = new Vector3(currentXValue, cachedY);
		if (currentHealth > maxHealth / 2) {
			visualHealth.color = new Color32((byte)Map(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
		}
		else {
			visualHealth.color = new Color32(255, (byte)Map(currentHealth, -50, maxHealth / 2, 0, 255), 0, 255);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Damage") {
			if (!onCD && currentHealth > 1) {
				StartCoroutine(CoolDownDmg());
				Health -= 1; 
			}
		}
		if (other.tag == "Health") {
			if (!onCD && currentHealth < maxHealth) {
				StartCoroutine(CoolDownDmg());
				Health += 1; 
			}
		}
	}
		
	// Keeps track of the  damage CD
	IEnumerator CoolDownDmg() {
		onCD = true; 
		yield return new WaitForSeconds(cooldown); //Waits a while before we are able to take dmg again
		onCD = false;
	}
		
	// This method maps a range of number into another range
	public float Map(float x, float in_min, float in_max, float out_min, float out_max) {
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}
}
