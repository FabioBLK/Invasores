using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {
	public GameObject explosion;
	public GameObject smallHit;
	public GameObject laserShot;
	public GameObject [] healthIcon;
	private GameObject [] healthIconObject = new GameObject[3];
	private int iconIndex=0;
	public float health = 350;
	public float padding = 1;
	public float shipXPos;
	public float laserSpeed=10;
	public int fireCounter=0;
	private float xmax = 5;
	private float xmin = -5;
	public float shipSpeed = 10;
	
	// android var
	private float moveX=0;
	
	//public GameObject enemyLaserShot;
	
	//public Camera camera = Camera.main.transform.position;
	
	
	// Use this for initialization
	void Start () {
		for (int i=0;i<3;i++){
		healthIconObject[i] = Instantiate (healthIcon[i],new Vector3(6,3+i,0),Quaternion.identity)as GameObject;
		}
	
		Camera camera = Camera.main;
		float camDistance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,camDistance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,1,camDistance)).x - padding;
			
		print (xmax);
		print (xmin);
	}
	
	// Update is called once per frame
	void Update () {	
	
		/* This control script works, but it has different behaviour
		
		private float dirControl;
		private float shipMove;
		
		dirControl = Input.GetAxis("Horizontal") * Time.deltaTime;
		shipMove += dirControl * shipSpeed;
		transform.position = new Vector3 (shipMove,transform.position.y, transform.position.z);
		**** End of the control Script*/
		
		
		
		/*start of player movement
		shipXPos = shipSpeed * Time.deltaTime;
		if (Input.GetKey(KeyCode.RightArrow)){
			transform.position = new Vector3(Mathf.Clamp (transform.position.x + shipXPos,xmin,xmax),transform.position.y,transform.position.z);
		} else if (Input.GetKey (KeyCode.LeftArrow)){
				transform.position = new Vector3(Mathf.Clamp (transform.position.x - shipXPos,xmin,xmax),transform.position.y,transform.position.z);
		}
		// end of player movement
		**** End of the regular control script */
		
		//Android FIRE Controls Start
		//for (int i = 0; i<Input.touchCount; i++){
		//Touch touch =Input.GetTouch(0);
		//	if (touch.phase == TouchPhase.Ended){
		if (Input.GetMouseButtonDown(0)){
		PlayerFire();
		}
		//	}
		//}
		
		//Android Controls End
		
		//Android Controls 1
		shipXPos = shipSpeed*Time.deltaTime;
		moveX = Mathf.Clamp(Input.acceleration.x*shipXPos,-0.3f,0.3f);
		//transform.position = new Vector3(moveX,transform.position.y,transform.position.z);
		transform.Translate (moveX,0,0);
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x,xmin,xmax),transform.position.y,transform.position.z);
		//End Android Controls 1*/
		

		
		/*
		//laser shot instantiate
		if (Input.GetKeyDown (KeyCode.Space)&&fireCounter<3){
			//InvokeRepeating("PlayerFire",0.0001f,0.2f);
			PlayerFire ();
		}
		//end of laser shot instantiate
		
		
		if (Input.GetKeyUp (KeyCode.Space)){
			CancelInvoke("PlayerFire");
		}
		*/

	}
	
	
	void PlayerFire(){
		GameObject laserUnity = Instantiate (laserShot, new Vector3(transform.position.x,transform.position.y+0.7f,transform.position.z),Quaternion.identity)as GameObject;
		laserUnity.rigidbody2D.velocity = new Vector3(0,laserSpeed,0);
		fireCounter++;
	}
	
	void OnTriggerEnter2D(Collider2D col){
		
		EnemyLaserShot enemyMissile = col.GetComponent<EnemyLaserShot>();
		if (enemyMissile){
			Instantiate (smallHit, new Vector3(transform.position.x,transform.position.y+0.6f,transform.position.z), Quaternion.identity);
			//Destroy (smallHit,3.0f);
			health-=enemyMissile.DamageTaken ();	
			enemyMissile.HitDone ();
		
			if (health<0){
				PlayerDefeat();
			}else{
				Destroy(healthIconObject[iconIndex]);
				iconIndex++;
			}
		}
	}
	
	void PlayerDefeat(){
		AudioClip enemyDestroyedSound;
		enemyDestroyedSound = gameObject.audio.clip;
		AudioSource.PlayClipAtPoint (enemyDestroyedSound, gameObject.transform.position);	
		LevelManager playerDestroyed;
		playerDestroyed = FindObjectOfType<LevelManager>();
		playerDestroyed.PlayerDestroyed ();
		explosion.particleSystem.duration.Equals(2.0f);
		Instantiate (explosion,transform.position,Quaternion.identity);
		Destroy (gameObject);
	}
}

