using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class BossAI : MonoBehaviour {

	private GameObject player;
	public Transform target;
	private Vector3 dir;
	
	private bool fireToggle = true; 
	
	private int AIState = 0; 
	
	public float a = 0.1f; //= 0.3f;
	public float b = 1f; //= 0.1f;

	
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerCharacter");
		Debug.Assert(player != null, "Boss AI : Not reading player char");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (AIState == 0) {
			//pop out of the ground and wait a bit before attacking 
			print("AIState0");
			//goes from y = 113 to y = 123 
			if (transform.position.y < 123) {
				transform.Translate(Vector3.up * Time.deltaTime * 3);
			}
			else {
				StartCoroutine(delay1());
			}
			
		}
		if (AIState == 1) {
			print("AIState1");
			//always look at the player 
			dir= target.position - transform.position;
			dir.Normalize();
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 50 * Time.deltaTime);
			
			//fire at player 
			if (Vector3.Distance(player.transform.position,transform.position) < 100) {

				if (fireToggle) {
					fireToggle = false; 
					Shoot();
				}

			}
		}
	}
	IEnumerator delay1() {
		yield return new WaitForSeconds(0.5f);
		RigidbodyFirstPersonController.disableController = false;
		PlayerBehavior.sceneState = 10;
		StartCoroutine(delay2());
	}
	
	IEnumerator delay2() {
		yield return new WaitForSeconds(0.5f);
		AIState = 1;
	}
	
	void Shoot() {
		EnemyProjectileShooter.canFire = true;
		StartCoroutine(waitABit());
	}
	IEnumerator waitABit() {
		yield return new WaitForSeconds(a); //duration of firing 
		EnemyProjectileShooter.canFire = false;
		StartCoroutine(waitABit2());
	}
	
	IEnumerator waitABit2() {
		yield return new WaitForSeconds(b); 
		fireToggle = true; 
	}
	
}
