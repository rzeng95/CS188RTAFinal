using UnityEngine;
using System.Collections;

public class PlayerBulletBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Enemy") {
			print ("enemy tag");
			GameObject target = other.gameObject;
			target.SendMessage("ApplyDamage", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Boss") {
			print ("boss tag");
			GameObject target = other.gameObject;
			target.SendMessage("ApplyDamageBoss", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
			
	}
}
