using UnityEngine;
using System.Collections;

public class EnemyLaserShot : MonoBehaviour {
	public float damage = 100.0f;
	
	void Start(){
		AudioSource.PlayClipAtPoint (gameObject.audio.clip, transform.position);
	}
	
	
	public void HitDone (){
		Destroy (gameObject);
	}
	
	public float DamageTaken(){
		return damage;
	}
	
}
