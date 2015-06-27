using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform target;
	
	private bool attackSwitch = false;
	private bool switcher = true;
	
	private GameObject player; 
	
	private int AIState;
	
	private Vector3 dir;
	
	private bool canFire = true;
	
	private int detectDistance;
	
	// Use this for initialization
	void Start () {

		player = GameObject.Find("PlayerCharacter");
		Debug.Assert(player != null, "Enemy AI : Not reading player char");
		
		AIState = 0;
		
		detectDistance = 35; //distance between player and AI before it starts shooting at player 
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AIState == 0) {
			
			if (Vector3.Distance(player.transform.position,transform.position) < detectDistance) {
				print("noticing player");
				AIState = 1;
			}	
			
			else {
				transform.Rotate(Vector3.up * Time.deltaTime*50);
				
				if (switcher) {
					StartCoroutine(delay());
					transform.Translate(Vector3.forward * Time.deltaTime*2);
				}
				if (!switcher){
					StartCoroutine(delay2());
					transform.Translate(Vector3.back * Time.deltaTime*2);			
				}
				
			}
		}
		if (AIState == 1) {
			if (Vector3.Distance(player.transform.position,transform.position) < detectDistance) {
				dir= target.position - transform.position;
				dir.Normalize();
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
				
				if (canFire) {
					canFire = false;
					attackSwitch = true;
					StartCoroutine(waitABit2());
				}
			}
			else 
				AIState = 0;
		}

		
		if (attackSwitch) {
			Shoot();
			attackSwitch = false;
		}

		
	}
	
	void Burst() {
		attackSwitch = true;
		StartCoroutine(waitABit2());
	}
	
	void Shoot() {
		EnemyProjectileShooter.canFire = true;
		StartCoroutine(waitABit());
	}
	
	IEnumerator waitABit() {
		yield return new WaitForSeconds(0.3f);
		EnemyProjectileShooter.canFire = false;
	}
	IEnumerator waitABit2() {
		
		yield return new WaitForSeconds(0.1f);
		attackSwitch = false;
		StartCoroutine(attackBreak());
	}
	IEnumerator attackBreak() {
		yield return new WaitForSeconds(3.2f);
		canFire = true;
	}
	
	IEnumerator delay() {
		yield return new WaitForSeconds(2f);
		switcher = false;
	}
	IEnumerator delay2() {
		yield return new WaitForSeconds(2f);
		switcher = true;
	}
	
}
