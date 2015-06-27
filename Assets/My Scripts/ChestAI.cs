using UnityEngine;
using System.Collections;

public class ChestAI : MonoBehaviour {
	
	public static bool canOpen = false;
	private bool once = true; 
	private GameObject lid; 
	// Use this for initialization
	void Start () {
		lid = GameObject.Find("/Chest/Chest/MainAxis");
	}
	
	// Update is called once per frame
	void Update () {

		if (canOpen && once) {
			StartCoroutine(delay());
			lid.transform.Rotate(Vector3.back, 90 * Time.deltaTime);
		}
	}
	IEnumerator delay() {
		yield return new WaitForSeconds(1f);
		once = false;
	}
}
