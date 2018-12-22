using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyBehaviour : MonoBehaviour {
	public float health=150f;
	public GameObject laserPrefab;
	public float projectileSpeed=5f;
	public float shotsPerSecond=0.5f;
	public int pointsPerUnit=150;
	private ScoreKeeper scoreKeeper;
	public AudioClip laserSound;
	public AudioClip deathSound;
	//private FormationController canIShoot;
	// Use this for initialization
	void Start () {
	//canIShoot = FindObjectOfType<FormationController>();
	scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(!canIShoot.movingDown){
		float shootChance = Time.deltaTime*shotsPerSecond;
		if(Random.value<shootChance){
			Shooting();
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
		scoreKeeper.Score(pointsPerUnit);
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
		Destroy(gameObject);
	}
	void Shooting(){
		//Vector3 projectileSpawn = transform.position - new Vector3 (0,1,0);
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f,-projectileSpeed,0f);
		AudioSource.PlayClipAtPoint(laserSound, transform.position);
	}
}
