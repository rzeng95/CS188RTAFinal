using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
	
	public static int sceneState = 0; //scene counter 
	public static bool isAlive = true; //keeps track of if player is alive or not (talks to health script)
	
	//private GameObject player;
	private GameObject playerCam;
	private GameObject startCam;
	
	private GameObject startGUI;
	private GameObject healthGUI;
	private GameObject crosshairCanvas;
	private GameObject deathCanvas;
	private GameObject endCanvas;
	
	private GameObject instrCanvas1;
	private GameObject instrCanvas2;
	private GameObject instrCanvas3;
	
	public Text playersubText;
	public Text friendsubText;
	
	public Transform respawnLoc; 
	private GameObject newrespawnLoc;
	public Transform bossLoc;
	public GameObject chestTrigger;
	private GameObject chest;
	
	private GameObject friendlySnowman;
	
	private GameObject playerGun;
	
	
	private GameObject boss;
	
	private bool once_1 = true;
	private bool once_2 = true;
	private bool once_3 = true;
	private bool once_4 = true;
	
	public static bool isBossDead = false;
	
	public GameObject baby1;
	public GameObject baby2;
	public GameObject baby3;
	public GameObject baby4;
	private bool baby1go = false;
	private bool baby2go = false;
	private bool baby3go = false;
	private bool baby4go = false;
	
	private bool endState = false;
	
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked; //at the beginning, lock cursor to middle of screen 
		Cursor.visible = false; //at the beginning, the cursor is hidden (since it's locked to middle of screen)
		
		/* Find all the GameObject we will be using - As opposed to making these variables public and drag-and-drop into inspector window */

		//player = GameObject.Find("PlayerCharacter");
		playerCam = GameObject.Find("/PlayerCharacter/MainCamera");
		startCam = GameObject.Find("StartCamera");
		startGUI = GameObject.Find("/Canvases/TitleCanvas");
		healthGUI = GameObject.Find("/Canvases/GUICanvas");
		crosshairCanvas = GameObject.Find("/Canvases/CrosshairCanvas");
		deathCanvas = GameObject.Find("/Canvases/DeathCanvas");
		endCanvas = GameObject.Find("/Canvases/EndCanvas");
		
		chest = GameObject.Find("Chest");
		
		instrCanvas1 = GameObject.Find("Canvases/InstructionCanvas1");
		instrCanvas2 = GameObject.Find("Canvases/InstructionCanvas2");
		instrCanvas3 = GameObject.Find("Canvases/InstructionCanvas3");
		
		newrespawnLoc = GameObject.Find("NewRespawnLoc");		
		
		
		friendlySnowman = GameObject.Find("FriendlySnowman");
		
		boss = GameObject.Find("BossSnowman");
		
		playerGun = GameObject.Find("/PlayerCharacter/MainCamera/GunContainer");
		//Debug.Log(player);
		//Debug.Log(playerCam);
		
		//Debug.Log(startCam);
		
		//Initialization 
		
		//Disable player cam since we are starting with the title screen
		playerCam.SetActive(false);
		
		//Disable crosshairs
		crosshairCanvas.SetActive(false);
		//Disable key
		
		//Disable GUI canvas 
		healthGUI.SetActive(false);
		//Enable title canvas
		startGUI.SetActive(true);
		
		//Disable death canvas
		deathCanvas.SetActive(false);
		
		//Hide player gun
		playerGun.SetActive(false);

		//Disable all instruction canvases
		instrCanvas1.SetActive(false);
		instrCanvas2.SetActive(false);
		instrCanvas3.SetActive(false);
		
		//Disable all subtitles
		playersubText.text = "";
		friendsubText.text = "";
		
		//Disable big boss
		boss.SetActive(false);
		
		//Disable end canvas 
		endCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		//NOTE: This is pretty broken. Cursor flashes on screen - hopefully this won't occur in the final build.
		//Documented on: http://forum.unity3d.com/threads/cursor-lockstate-troubles.278221/#post-1887455
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else if (!endState) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;			
		}
		
		//This prints out the current scene state
		Debug.Log("Scene State:" + sceneState);
		
		
		if (isAlive) {
			//Player is alive; keep track of the scene and do different things depending on scene state (see above)
			
			if (sceneState == 0) {
				if (Input.GetKeyDown(KeyCode.Return)) {
					startGUI.SetActive(false);
					sceneState = 1;
				}
			}
			if (sceneState == 1) {
				//Camera rotates downwards, then translates backwards a bit 
				if (startCam.transform.eulerAngles.x < 348)
					startCam.transform.Rotate(Vector3.right, 13 * Time.deltaTime);
				else {
					if (startCam.transform.position.z > 87.5) 
						startCam.transform.Translate(Vector3.back * Time.deltaTime);
					else
						StartCoroutine(s1d1());
				}
			}
			if (sceneState == 2 && once_1 == true ) {
				once_1 = false; //execute this only once 
				startGUI.SetActive(false); 
				startCam.SetActive(false);
				playerCam.SetActive(true);
				healthGUI.SetActive(true);
				
				//Start draining temp from player
				HealthControl.canDrain = true; 
				
				StartCoroutine(s2d1());

			}
			if (sceneState == 3 && once_2 == true) {
				once_2 = false;
				instrCanvas1.SetActive(true);
				StartCoroutine(s3d1());
			}
			
			if (sceneState == 4) {
				if (Vector3.Distance(friendlySnowman.transform.position,transform.position) < 3.5) {
					RigidbodyFirstPersonController.disableController = true;
					sceneState = 5;				
				}	
			}
			
			if (sceneState == 5) {			
				//look at the snowman 
				Vector3 dir = friendlySnowman.transform.position - transform.position;
				dir.Normalize();
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 8 * Time.deltaTime);
				
				StartCoroutine(s5d1());
			}
			
			if (sceneState == 6 && once_3 == true) {
				once_3 = false;
				
				respawnLoc.transform.position = newrespawnLoc.transform.position;
				
				StartCoroutine(s6d1());
			}
			
			
			if (sceneState == 7 && once_4 == true) {
				once_4 = false;
				RigidbodyFirstPersonController.disableController = false;
				
				playerGun.SetActive(true);
				StartCoroutine(s7d1());
			}
			
			if (sceneState == 8) {
				//Keep walking until we trigger the boss spawn 
				if(Vector3.Distance(bossLoc.transform.position,transform.position) < 35) {
					RigidbodyFirstPersonController.disableController = true;
					sceneState = 9;
				}

			}
			
			if (sceneState == 9) {
				//Activate the boss, play the small cutscene 
				boss.SetActive(true);
				
				Vector3 dir2 = boss.transform.position - transform.position;
				dir2.Normalize();
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir2), 8 * Time.deltaTime);
				
			}
			if (sceneState == 10) {
				//fight scene with boss taking place...
				//check if boss is dead, if so then continue
				if (isBossDead) {
					print ("registered dead boss");
					sceneState = 11;
				}
			}
			if (sceneState == 11) {
				if (Vector3.Distance(chestTrigger.transform.position,transform.position) < 1) {
					RigidbodyFirstPersonController.disableController = true;
					Vector3 dir3 = chest.transform.position - transform.position;
					dir3.Normalize();
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir3), 8 * Time.deltaTime);
					StartCoroutine(s11d1());
				}
				
			}
			if (sceneState == 12) {
				//in the correct position. make the key disappear 
				StartCoroutine(s12d1());
			}
			if (sceneState == 13) {
				//chest has opened; now raise the mini snowmen 
				StartCoroutine(s13d1());
				if (baby1go) {
					if (baby1.transform.position.y < 122.2) {
						baby1.transform.Translate(Vector3.up * Time.deltaTime * 3);
					}
				}
				if (baby2go) {
					if (baby2.transform.position.y < 122.2) {
						baby2.transform.Translate(Vector3.up * Time.deltaTime * 3);
					}
				}
				if (baby3go) {
					if (baby3.transform.position.y < 122.2) {
						baby3.transform.Translate(Vector3.up * Time.deltaTime * 3);
					}
				}
				if (baby4go) {
					if (baby4.transform.position.y < 122.2) {
						baby4.transform.Translate(Vector3.up * Time.deltaTime * 3);
					}
				}				
			}
			if (sceneState == 14) {
				endState = true;
				endCanvas.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			
		}
		else {
			//Player is dead; spawn at gazebo 
			print ("im dead!");

			RigidbodyFirstPersonController.disableController = true;
			ProjectileShooter.disableShooter = true;
			StartCoroutine(death());
			deathCanvas.SetActive(true);
		}

	}
	/* COROUTINE FUNCTIONS */
	IEnumerator s1d1() {
		//After camera moves to correct position, wait a bit before passing control to player 
		yield return new WaitForSeconds(2);
		sceneState = 2;
	}
	
	IEnumerator s2d1() {
		yield return new WaitForSeconds(2);
		playersubText.text = "It's getting pretty cold. I should find someplace warm.";
		yield return new WaitForSeconds(3);
		playersubText.text = "";
		yield return new WaitForSeconds(3);
		sceneState = 3; 
	}
	
	IEnumerator s3d1() {
		yield return new WaitForSeconds(2.5f); //wait 2.5 seconds then turn off instr1 
		instrCanvas1.SetActive(false);
		StartCoroutine(s3d2());
	}
	
	IEnumerator s3d2() {
		yield return new WaitForSeconds(2); //2 seconds later, turn on instr2
		instrCanvas2.SetActive(true);
		StartCoroutine(s3d3());
	}
	IEnumerator s3d3() {
		yield return new WaitForSeconds(2.5f); //2.5 seconds later, turn off instr2
		instrCanvas2.SetActive(false);
		
		sceneState = 4;
	}
	IEnumerator s5d1() {
		yield return new WaitForSeconds(1f);
		sceneState = 6;
	}
	IEnumerator s6d1() {
		yield return new WaitForSeconds(1f); 
		friendsubText.text = "Hey there!";
		yield return new WaitForSeconds(1f);
		friendsubText.text = "";
		yield return new WaitForSeconds(2f); 
		playersubText.text = "Whoa, a talking snowman! Where am I?";
		yield return new WaitForSeconds(2f);
		playersubText.text = "";
		yield return new WaitForSeconds(2f);
		friendsubText.text = "No idea. But take this golden key!";
		yield return new WaitForSeconds(2f);
		friendsubText.text = "";
		yield return new WaitForSeconds(1f);
		sceneState = 7;
	}
	
	IEnumerator s7d1() {
		yield return new WaitForSeconds(2f); 
		instrCanvas3.SetActive(true);
		yield return new WaitForSeconds(2f); 
		instrCanvas3.SetActive(false);
		yield return new WaitForSeconds(1f); 
		crosshairCanvas.SetActive(true);
		sceneState = 8;
		
	}
	
	IEnumerator s11d1() {
		yield return new WaitForSeconds(1f);
		sceneState = 12;
	}
	
	IEnumerator s12d1() {
		yield return new WaitForSeconds(1f);
		playerGun.SetActive(false);
		yield return new WaitForSeconds(1f);
		ChestAI.canOpen = true;
		sceneState = 13;
	}
	
	IEnumerator s13d1() {
		yield return new WaitForSeconds(0.5f);
		baby1go = true;
		yield return new WaitForSeconds(0.5f);
		baby2go = true;
		yield return new WaitForSeconds(0.5f);
		baby3go = true;
		yield return new WaitForSeconds(0.5f);
		baby4go = true;
		yield return new WaitForSeconds(1f);
		sceneState = 14;
	}
	
	IEnumerator death() { //fade screen and teleport player to respawn point 
		yield return new WaitForSeconds(1.6f);
		transform.position = respawnLoc.position;
		StartCoroutine(respawn());
	}
	IEnumerator respawn() { //then remove the black screen and reset player stats 
		yield return new WaitForSeconds(0.5f);
		deathCanvas.SetActive(false);
		HealthControl.playerCurHP = 100;	
		isAlive = true;	
		StartCoroutine(respawn2());
	}
	IEnumerator respawn2() { //wait a bit and give player functions back
		yield return new WaitForSeconds(0.1f);
		ProjectileShooter.disableShooter = false;	
		RigidbodyFirstPersonController.disableController = false;
	}
	
}
