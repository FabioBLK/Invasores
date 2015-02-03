using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	public float enemyHealth = 150;
	public float minShotRate=0.1f;
	public float maxShotRate=3.0f;
	private string enemyName;
	public GameObject explosion;
	public GameObject smallHit;
	private ScoreControl textAdd;
	public int scoreValue = 100;
	//public GameObject playerShot;
	public GameObject enemyLaserShot;
	public float enemyShotSpeed = -5;
	private float shotRate = Random.Range(1.0f,3.0f);
	
	void Start(){
		enemyName = gameObject.name;
		print (enemyName);
		textAdd = FindObjectOfType<ScoreControl>();
	}
	
	void Update() {
		shotRate-=Time.deltaTime;
		if (shotRate<0){
			//EnemyFire();
			EnemyFire();
			shotRate=Random.Range(minShotRate,maxShotRate);
		}
	} 	
	
	void EnemyFire(){
		if (enemyName=="Enemy(Clone)"){
			EnemySingleFire();
		}
		if (enemyName=="Enemy2(Clone)"){
			EnemySingleFire();
		}  
		if (enemyName=="Enemy3(Clone)"){
			Enemy3WayFire();
		} 
	}
	
	void EnemySingleFire(){
		GameObject enemyShotUnity = Instantiate(enemyLaserShot, new Vector3(transform.position.x,transform.position.y-0.7f,transform.position.z), Quaternion.identity) as GameObject;
		enemyShotUnity.rigidbody2D.velocity = new Vector3(0,enemyShotSpeed,0);
	}
	
	void Enemy3WayFire(){
		GameObject enemyShotUnity1 = Instantiate(enemyLaserShot, new Vector3(transform.position.x,transform.position.y-0.7f,transform.position.z), Quaternion.AngleAxis (30,Vector3.forward)) as GameObject;
		enemyShotUnity1.rigidbody2D.velocity = new Vector3(3f,-5,0);
		GameObject enemyShotUnity2 = Instantiate(enemyLaserShot, new Vector3(transform.position.x,transform.position.y-0.7f,transform.position.z), Quaternion.identity) as GameObject;
		enemyShotUnity2.rigidbody2D.velocity = new Vector3(0,-5,0);
		GameObject enemyShotUnity3 = Instantiate(enemyLaserShot, new Vector3(transform.position.x,transform.position.y-0.7f,transform.position.z), Quaternion.AngleAxis (-30,Vector3.forward)) as GameObject;
		enemyShotUnity3.rigidbody2D.velocity = new Vector3(-3f,-5,0);
	}
	
	void OnTriggerEnter2D(Collider2D col){	
		LaserShot missile = col.gameObject.GetComponent<LaserShot>();
		
		if (missile){
		Instantiate (smallHit, new Vector3(transform.position.x,transform.position.y-0.4f,transform.position.z), Quaternion.identity);
		//Destroy (smallHit,3.0f);
		missile.HitDone();
		enemyHealth -= missile.DamageTaken ();
		if (enemyHealth<=0){
			EnemyDefeat();
		}
		}
	}
	
	void EnemyDefeat(){
		AudioClip enemyDestroyedSound = gameObject.audio.clip;
		AudioSource.PlayClipAtPoint (enemyDestroyedSound, gameObject.transform.position,0.3f);
		textAdd.ScoreAdding(scoreValue);
		Instantiate (explosion,gameObject.transform.position,Quaternion.identity);
		EnemySpawn enemyCunter;
		enemyCunter = FindObjectOfType<EnemySpawn>();
		enemyCunter.enemyCount += 1;
		Destroy (gameObject);
	}
}
