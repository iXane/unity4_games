using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float hp = 300f;
	public float shipMovingSpeed = 15.0f;
	public GameObject projectile;
	
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float padding = 1f;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	float xmin;
	float xmax;
	
	// Initializing stuff //
	void Start()
	{
		// Getting the distance between the camera and the object //
		float distance = transform.position.z - Camera.main.transform.position.z;
		
		// 0 is the left side, 0.5 is the middle and 1.0 is the right side of the camera //
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		
		// Assigning the values so this is the areas where the player can move //
		xmin = leftmost.x + padding; // left area wall
		xmax = rightmost.x - padding; // right area wall
	}
	
	// Update is called once per frame
	void Update () {
		moveWithKeyboard();
	}
	
	void Fire()
	{
		Vector3 offset = new Vector3 (0, 0.8f, 0);
		GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	// With the push of a button, we're gonna check where we move.
	void moveWithKeyboard()
	{					
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
							
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//transform.position += new Vector3(-shipMovingSpeed * Time.deltaTime, 0, 0);
			// Moving the ship to the left, depending independent on the framerate //
			transform.position += Vector3.left * shipMovingSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{			
			// Moving the ship to the right, depending independent on the framerate //
			transform.position += Vector3.right * shipMovingSpeed * Time.deltaTime;
		}
		
		// Restrict the player to the gamespace //
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		EnemyLaser missile = col.gameObject.GetComponent<EnemyLaser>();
		
		if (missile)
		{
			hp -= missile.GetDamage();
			missile.Hit();
			
			if (hp <= 0)
			{
				Die ();
			}
		}
	
	}
	
	void Die()
	{
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
		//LevelManager().LoadLevel("Start Menu");
	}
}
