using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript2 : MonoBehaviour
{
	#region FIELDS

	/// <summary>
	/// The Player's movement speed
	/// </summary>
	public float speed;

	/// <summary>
	/// The players current health
	/// </summary>
	private int currentHealth;

	/// <summary>
	/// The player's max health
	/// </summary>
	public int maxHealth;

	/// <summary>
	/// The healt's transform, this is used for moving the object
	/// </summary>
	public RectTransform healthTransform;

	/// <summary>
	/// The health text
	/// </summary>
	public Text healthText;

	/// <summary>
	/// The health's image, this is used for color changing
	/// </summary>
	public Image visualHealth;

	/// <summary>
	/// The health's y pos
	/// </summary>
	private float cachedY;

	/// <summary>
	/// The minimum value of the health's x pos
	/// </summary>
	private float minXValue;

	/// <summary>
	/// The max value of the health's x pos
	/// </summary>
	private float maxXValue;

	/// <summary>
	/// The current xValue of the health
	/// </summary>
	private float currentXValue;

	/// <summary>
	/// How often can I take damage
	/// </summary>
	public float cooldown;

	/// <summary>
	/// Indicates if the we can take damage or not
	/// </summary>
	private bool onCD;

	/// <summary>
	/// The healthbar's canvas
	/// </summary>
	public Canvas canvas;

	int i = 0;

	#endregion

	#region PROPERTIES

	/// <summary>
	/// Property for accessing the players health
	/// </summary>
	public int Health
	{
		get { return currentHealth; }
		set
		{
			currentHealth = value;
			HandleHealthbar();
		}
	}

	#endregion



	// Use this for initialization
	void Start()
	{   
		//Sets all start values

		onCD = false;
		cachedY = healthTransform.position.y; //Caches the healthbar's start pos
		maxXValue = healthTransform.position.x; //The max value of the xPos is the start position
		minXValue = healthTransform.position.x - healthTransform.rect.width*canvas.scaleFactor; //The minValue of the xPos is startPos - the width of the bar
		currentHealth = maxHealth; //Sets the current healt to the maxHealth
	}

	// Update is called once per frame
	void Update()
	{   
		Health = (int)ChangeCharacter.hp2;
		healthText.text = ChangeCharacter.name2;

		//Makes sure that the player moves
		HandleMovement();
	}

	/// <summary>
	/// Handels the players movement
	/// </summary>
	private void HandleMovement()
	{   
		//Used for making the movement framerate independent
		float translation = speed * Time.deltaTime;

		transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
	}

	/// <summary>
	/// Handles the healthbar by moving it and changing color
	/// </summary>
	private void HandleHealthbar()
	{   
		//Writes the current health in the text field
		healthText.text = "Health: " + currentHealth;

		//Maps the min and max position to the range between 0 and max health
		currentXValue = Map(currentHealth, 0, maxHealth, minXValue, maxXValue);

		//Sets the position of the health to simulate reduction of health
		healthTransform.position = new Vector3(currentXValue, cachedY);

		if (currentHealth > maxHealth / 2) //If we have more than 50% health we use the Green colors
		{
			visualHealth.color = new Color32((byte)Map(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
		}
		else //If we have less than 50% health we use the red colors
		{
			visualHealth.color = new Color32(255, (byte)Map(currentHealth, -50, maxHealth / 2, 0, 255), 0, 255);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Damage") //Used for simulating taking damage
		{
			if (!onCD && currentHealth > 1)
			{
				StartCoroutine(CoolDownDmg()); //Makes sure that we can't take damage right away
				Health -= 1; //Uses the Health Property to so that we recolor and rescale the health when we change it
			}
		}
		if (other.tag == "Health") //Used for simulating gaining health
		{
			if (!onCD && currentHealth < maxHealth)
			{
				StartCoroutine(CoolDownDmg()); //Makes sure that we can't take damage right away
				Health += 1; //Uses the Health Property to so that we recolor and rescale the health when we change it
			}
		}
	}

	/// <summary>
	/// Keeps track of the  damage CD
	/// </summary>
	/// <returns></returns>
	IEnumerator CoolDownDmg()
	{
		onCD = true; 
		yield return new WaitForSeconds(cooldown); //Waits a while before we are able to take dmg again
		onCD = false;
	}

	/// <summary>
	/// This method maps a range of number into another range
	/// </summary>
	/// <param name="x">The value to evaluate</param>
	/// <param name="in_min">The minimum value of the evaluated variable</param>
	/// <param name="in_max">The maximum value of the evaluated variable</param>
	/// <param name="out_min">The minum number we want to map to</param>
	/// <param name="out_max">The maximum number we want to map to</param>
	/// <returns></returns>
	public float Map(float x, float in_min, float in_max, float out_min, float out_max)
	{
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}
}
