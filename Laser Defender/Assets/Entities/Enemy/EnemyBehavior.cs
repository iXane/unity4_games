using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float health = 100;
	public int ScoreValue = 75;
	public GameObject EnemyProjectile;
	public float EnemyProjectileSpeed = -5f;
	public float shotsPerSecond = 0.5f;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private Score scoreKeeper;
	
	void OnTriggerEnter2D(Collider2D col)
	{		
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		
		if (missile)
		{
			health -= missile.GetDamage();
			missile.Hit();
			
			if (health <= 0)
			{ 
				Die();
			}
			
		}
	}
	
	void Die()
	{
		scoreKeeper.AddPoints(ScoreValue);
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
	}
	
	void FireAtPlayer()
	{
		Vector3 startPosition = transform.position + new Vector3(0f, -0.8f, 0f);
		GameObject beam = Instantiate(EnemyProjectile, startPosition, Quaternion.identity)as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0f, EnemyProjectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void Start()
	{
		scoreKeeper = GameObject.Find("Score").GetComponent<Score>();
	}
	
	void Update()
	{		
		float probability = Time.deltaTime * shotsPerSecond;
	
		if (Random.value < probability)
			FireAtPlayer();
			
//		InvokeRepeating("FireAtPlayer", 0.000001f, 0.2f);
//		
//		if (Random.value >= 0.01f)
//			CancelInvoke("FireAtPlayer");
			
			//FireAtPlayer();
	}
	
	
}
