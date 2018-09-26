using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

	private Paddle paddle;
	private Vector3 paddleToBallVector;
	private bool hasStarted = false;

	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		// Setting the ball position on the paddle as soon as the game starts //
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	void OnCollisionEnter2D(Collision2D collission)
	{
		Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
	
		// Ball does not trigger sound when brick is destroyed. //
		// Not 100% sure why, possibly because brick isn't there //
		if(hasStarted)
		{
			audio.Play();
			rigidbody2D.velocity += tweak;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted)
		{
			// Lock the ball relative to the paddle //
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			// Waiting for player to launch the ball //
			if (Input.GetMouseButtonDown(0))
			{
				this.rigidbody2D.velocity = new Vector2(2f, 10f);
				hasStarted = true;
				//print ("Mouse clicked, launch ball");
			}
		}
	}
}
