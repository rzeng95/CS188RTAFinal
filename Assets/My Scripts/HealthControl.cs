using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthControl : MonoBehaviour {

	public Slider slider;
	private float playerMaxHP;
	public static float playerCurHP;
	
	public static bool canDrain = false;
	
	// Use this for initialization
	void Start () {
		playerMaxHP = 100;
		playerCurHP = 100;
		slider.minValue = 0;
		slider.maxValue = playerMaxHP;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = playerCurHP;
		if (canDrain)
			playerCurHP -= 0.04f; //natural drain from coldness 
		
		if (playerCurHP <= 0) {
			PlayerBehavior.isAlive = false; 
		}
	}
	
	void ApplyDamageToPlayer() {
		playerCurHP -= 5f; //when enemy lands hit on player 
	}
	void ApplyFireCostToPlayer() {
		playerCurHP -= 2f; //when player fires gun 
		print("taking fire cost damage");
	}
	void ApplyDrain() {
		canDrain = true;
	}
	void OnTriggerStay(Collider other) {
		
		if (other.gameObject.tag == "Warm") {
			print ("warm");
			if (playerCurHP <100) 
				playerCurHP += 0.1f;
		}
		
		if (other.gameObject.tag == "Water") {
			print ("in water");
			playerCurHP -= 0.3f;
		}
		
			
	}
}
