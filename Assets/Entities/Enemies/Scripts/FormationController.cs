using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float formationWidth;
	public float formationHeight;
	private bool movingRight=true;
	public bool  movingDown=true;
	public float enemySpeed;
	private float xMax,xMin;
	//public float enterSpeed;
	//private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		//Vector3 topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distanceToCamera));
		xMax = rightBoundary.x;
		xMin = leftBoundary.x;
		//yMin = topBoundary.y;
		//startPosition = transform.position;
		SpawnUntilFull();
	}

	/*void SpawnEnemies(){
		transform.position = startPosition;
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab,child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	*/

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab,freePosition.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull",1f);
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position,new Vector3(formationWidth,formationHeight,0));
	}

	// Update is called once per frame
	void Update () {
	/*	float bottomEdgeofFormation = transform.position.y + 0.5f *formationHeight;
		if(bottomEdgeofFormation>yMin){
			movingDown = true;
		} else{
			movingDown = false;
	*/
		if(movingRight){
			transform.position += Vector3.right * enemySpeed * Time.deltaTime;
			//transform.position += new Vector3(enemySpeed*Time.deltaTime,0,0);

			}else{
			transform.position +=Vector3.left *enemySpeed *Time.deltaTime;
			}

		/*if(movingDown){
			transform.position += Vector3.down * enterSpeed * Time.deltaTime;
			}
		*/

		float leftEdgeOfFormation = transform.position.x - 0.5f * formationWidth;
		float rightEdgeOfFormation = transform.position.x + 0.5f * formationWidth;
		if(leftEdgeOfFormation <= xMin){
			movingRight = true;
		}else if(rightEdgeOfFormation >=xMax){
			movingRight = false;
		}
		if(AllMembersDead()){
			SpawnUntilFull();
		}
	}


	Transform NextFreePosition(){
		foreach(Transform childPosition in transform){
			if(childPosition.childCount == 0){
				return childPosition;
			}
		}
		return null;
	}

	bool AllMembersDead(){
		foreach(Transform childPosition in transform){
			if(childPosition.childCount > 0){
				return false;
			}
		}
		return true;

	}
}
