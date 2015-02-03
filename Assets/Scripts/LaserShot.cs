using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {
	public float damage = 100.0f;
	private PlayerShip counter;
void Start(){
	counter = FindObjectOfType<PlayerShip>();
	AudioSource.PlayClipAtPoint (gameObject.audio.clip, transform.position);
}

public void HitDone (){
	Destroy (gameObject);
}

public float DamageTaken(){
	return damage;
}

void OnDestroy(){
	counter.fireCounter--;
}

void Update(){
	if(!renderer.isVisible){
		Destroy(gameObject);
	}
}

}
