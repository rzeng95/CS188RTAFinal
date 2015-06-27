using UnityEngine;
using System.Collections;

public class EnemyProjectileShooter : MonoBehaviour {

	private GameObject prefab;
	public static bool canFire;
	// Use this for initialization
	void Start () {
		prefab = Resources.Load("projectile_enemy") as GameObject;
		canFire = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (canFire) {
			GameObject projectile = Instantiate(prefab) as GameObject;
			projectile.transform.position = transform.position + transform.right;

			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			rb.velocity = transform.right * 40;
				
			Destroy(projectile, 3f);
		}
	}
}
