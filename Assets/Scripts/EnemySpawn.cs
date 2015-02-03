using UnityEngine;
using System.Collections;



public class EnemySpawn : MonoBehaviour {
	public int enemyCount = 0;
	public float speed = 1;
	private int enemyNumber =1;
	private float xPos=1;
	private float xSoma;
	public float maxHight = 1.0f;
	public float maxWidth = 1.0f;
	public float spawnDelay = 0.5f;
	private float xMax = 1.0f;
	private float xMin = 1.0f;
	public GameObject enemyPrefab;
	public GameObject enemy2Prefab;
	public GameObject enemy3Prefab;
	private bool direita = true;
	// Use this for initialization
	void Start () {
	
		Camera camera = Camera.main;
		float camDistance = transform.position.z - camera.transform.position.z;
		xMin = camera.ViewportToWorldPoint(new Vector3(0,0,camDistance)).x+6;
		xMax = camera.ViewportToWorldPoint(new Vector3(1,1,camDistance)).x-6;

		SpawnUntilFull();
	}
	
	void MoveDireita(){
		
		xSoma += xPos * Time.deltaTime * speed;
		transform.position = new Vector3(Mathf.Clamp(xSoma,xMin,xMax),transform.position.y,transform.position.z);
		if (transform.position.x >= xMax){
			direita=false;
		}
	}
	
	void MoveEsquerda(){
		
		xSoma -= xPos * Time.deltaTime * speed;
		transform.position = new Vector3(Mathf.Clamp(xSoma,xMin,xMax),transform.position.y,transform.position.z);
		if (transform.position.x <= xMin){
			direita=true;
		}
	} 
	
	// Update is called once per frame
	void Update () {
		
		//move os inimigos da esquerda para a direita
		print (direita);
		if (direita==true){
			MoveDireita();
		} else if (direita ==false){
			MoveEsquerda();
		}
		//
			if (AllEnemiesAreDead()){
				SpawnUntilFull();
			}
		print (enemyCount);
			
	}
	
	void NewSpawn (){
		foreach(Transform newEnemies in transform){
			GameObject respawnEnemies = Instantiate (enemyPrefab.gameObject,newEnemies.transform.position,Quaternion.identity)as GameObject;
			respawnEnemies.transform.parent = newEnemies;
		}
	}
	
	bool AllEnemiesAreDead (){
		foreach(Transform position in transform){
			if (position.childCount > 0){
				return false;
			}
		}
		return true;	
	}
	
	GameObject EnemyChoose(){
		if (enemyNumber<10){
			return enemyPrefab;
		}
		if (enemyNumber>10&&enemyNumber<30){
			return enemy2Prefab;
		}
		if (enemyNumber>30){
			return enemy3Prefab;
		}
		return enemyPrefab;
	}
	
	void SpawnUntilFull(){
		enemyNumber=Random.Range(1,enemyCount);
		Transform freePos = NextFreePosition ();
		GameObject respawnEnemies = Instantiate(EnemyChoose (),freePos.position,Quaternion.identity) as GameObject;
		respawnEnemies.transform.parent = freePos;
		if (FreePositionExists ()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	bool FreePositionExists(){
		foreach(Transform position in transform){
			if (position.childCount <= 0){
				return true;
			}
		}
		return false;
	}
	
	Transform NextFreePosition(){
		foreach(Transform position in transform){
			if(position.childCount<=0){
				return position;
			}
		}
		return null;
	}
	
	void OnDrawGizmos(){
		float xmin = transform.position.x -10 * maxHight;
		float xmax = transform.position.x +10 * maxHight;
		float ymin = transform.position.y -5 * maxWidth;
		float ymax = transform.position.y +5 * maxWidth;
		Gizmos.DrawLine (new Vector3(xmin,ymin,0), new Vector3(xmax,ymin,0));
		Gizmos.DrawLine (new Vector3(xmax,ymin,0), new Vector3(xmax,ymax,0));
		Gizmos.DrawLine (new Vector3(xmin,ymax,0), new Vector3(xmax,ymax,0));
		Gizmos.DrawLine (new Vector3(xmin,ymax,0), new Vector3(xmin,ymin,0));
	}
}
