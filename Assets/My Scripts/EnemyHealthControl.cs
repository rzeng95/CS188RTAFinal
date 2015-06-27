using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealthControl : MonoBehaviour {

	public Slider slider;
	private float enemyMaxHP;
	private float enemyCurHP;
	
	private bool once = true;
	private bool playDeathAnim = false;
	
	public bool isBoss = false;
	
	// Use this for initialization
	void Start () {
		enemyMaxHP = 100;
		enemyCurHP = 100;
		slider.minValue = 0;
		slider.maxValue = enemyMaxHP;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = enemyCurHP;
		
		if (once && enemyCurHP <= 0) {
			once = false;
			StartCoroutine(waitABit());
		}
		if (playDeathAnim) {
			Destroy(gameObject, 0.1f);
			
			if (isBoss) PlayerBehavior.isBossDead = true;
			
			playDeathAnim = false;
		}
	}
	
	void ApplyDamage() {
		enemyCurHP -= 10f;
	}
	void ApplyDamageBoss() {
		enemyCurHP -= 4f;
	}	
	IEnumerator waitABit() {
		yield return new WaitForSeconds(0.4f);
		playDeathAnim = true;	
	}

	
}
