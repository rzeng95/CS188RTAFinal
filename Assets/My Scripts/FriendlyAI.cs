using UnityEngine;
using System.Collections;

public class FriendlyAI : MonoBehaviour {

	public Transform target;
	
	private GameObject player;
	
	private Vector3 dir;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerCharacter");
		Debug.Assert(player != null, "Friendly AI : Not reading player char");
		
	}
	
	// Update is called once per frame
	void Update () {
		dir= target.position - transform.position;
		dir.Normalize();
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
	}
}
