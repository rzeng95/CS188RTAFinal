using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

	private GameObject prefab;
	public static int weaponID;
	public static bool disableShooter;
	// Use this for initialization
	void Start () {
		prefab = Resources.Load("projectile") as GameObject;
	
		weaponID = 0; 
		
		disableShooter = false;
	}
	
	// Update is called once per frame
	void Update () {
		//ToDo: this is just testing, get rid of this 
		if (Input.GetKeyDown(KeyCode.X)) {
			weaponID = 1;
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			weaponID = 0;
		}
		if (disableShooter == false) {
			if (weaponID == 0) { //semi auto 
				if (Input.GetKeyDown(KeyCode.Mouse0)) { //getkeydown only activates on first press
					fire();
				}
			}
			if (weaponID == 1) { //full auto
				if (Input.GetKey(KeyCode.Mouse0)) { //getkey applies for every frame the mouse button is held down 
					fire();
				}		
			}
		}
	}
	void fire() {
		//drain player health 
		GameObject target = GameObject.Find("PlayerCharacter");
		target.SendMessage("ApplyFireCostToPlayer", SendMessageOptions.DontRequireReceiver);
		
		//instantiate projectile object 
		GameObject projectile = Instantiate(prefab) as GameObject;
		projectile.transform.position = transform.position + Camera.main.transform.forward;

		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		rb.velocity = Camera.main.transform.forward * 40;
		
		Destroy(projectile, 3f);		
	}
}
