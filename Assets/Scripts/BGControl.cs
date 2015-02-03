using UnityEngine;
using System.Collections;

public class BGControl : MonoBehaviour {
	private int bgIndex = 0;
	private float moveDown = 1;
	private float xpos;
	public GameObject [] teste;
	// Use this for initialization
	void Start () {
		xpos = Random.Range(-6.0f,8.0f);
		transform.position=new Vector3(xpos,8,20);
		if (bgIndex<teste.Length){
			BGStart (bgIndex);
		}else bgIndex=0;

	}
	
	void BGStart(int count){
		
		
		//teste = new GameObject[2];
		GameObject objeto = Instantiate (teste[count],transform.position,Quaternion.identity)as GameObject;
		objeto.transform.parent = transform;
		moveDown = transform.position.y;
		bgIndex++;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.childCount>=1){
			moveDown-=Time.deltaTime*0.3f;
			transform.position=new Vector3(xpos,moveDown,transform.position.z);
		}
		if (transform.childCount<1){
			
			Start ();
		}
	}
}
