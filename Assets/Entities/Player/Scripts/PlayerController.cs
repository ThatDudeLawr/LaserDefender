using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GameObject laserPrefab;
	private float xMin,xMax,yMax,yMin;
	//padding is a small distance input for our ship coordinates to not get out of the camera vision at all
	private float padding=0.5f;
	public float projectileSpeed;
	public float firingRate;
	public float health;
	public AudioClip laserSound;

	// Use this for initialization
	void Start () {
		//Distance represents the distance between the camera and the desire object, and the x,y fields are the coordinates of the camera view(0,0 - bottom left; 1,1 - top right)
		float distance = transform.position.y - Camera.main.transform.position.y;
		Vector3 minLimit = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,distance));
		Vector3 maxLimit = Camera.main.ViewportToWorldPoint(new Vector3(1f,1f,distance));

		xMin = minLimit.x+padding;
		xMax = maxLimit.x-padding;
		yMin = minLimit.y+padding;
		yMax = maxLimit.y-padding;

	}
	
	// Update is called once per frame
	void Update () {
		Movement();
		float newX = Mathf.Clamp(transform.position.x,xMin,xMax);
		float newY = Mathf.Clamp(transform.position.y,yMin,yMax);
		transform.position = new Vector3(newX,newY,transform.position.z);
		
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Shooting",0.001f,firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Shooting");
		}
		
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health<=0){
				Die();
			}
		}
	}

	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}

	void Shooting(){
		//Vector3 offset = new Vector3(0,1,0);
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f,projectileSpeed,0f);
		AudioSource.PlayClipAtPoint(laserSound, transform.position);
	}

	void Movement(){
		if	(Input.GetKey(KeyCode.A)){
			//transform.position += new Vector3(-speed * Time.deltaTime,0,0);

			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.D)){
			transform.position += new Vector3(speed * Time.deltaTime,0,0);
		}	

		if(Input.GetKey(KeyCode.W)){
			transform.position += new Vector3(0,speed * Time.deltaTime,0);
		}

		if(Input.GetKey(KeyCode.S)){
			transform.position += new Vector3(0,-speed * Time.deltaTime,0);

		}
	}
}
