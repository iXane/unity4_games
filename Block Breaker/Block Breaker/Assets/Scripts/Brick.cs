using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public AudioClip crack;
	public static int breakableCount = 0;
	public Sprite[] hitSprites;
	public GameObject smoke;
	
	private LevelManager levelManager;
	private int timesHit;
	private bool isBreakable;
	
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		
		// Keep track of breakable bricks //
		if (isBreakable)
			breakableCount++;
			//print (breakableCount);
			
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void OnCollisionEnter2D(Collision2D collission)
	{
		AudioSource.PlayClipAtPoint(crack, transform.position);
		
		if (isBreakable)
			HandleHits();
	}
	
	void HandleHits()
	{
		// Increase times hit //
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits)
		{
			breakableCount--;
			//print(breakableCount);
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		}
		else
		{
			LoadSprites();
		}
	}
	
	void PuffSmoke()
	{
		GameObject smokePuff = Instantiate (smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = this.gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites()
	{
		int spriteIndex = timesHit - 1;
		
		if (hitSprites[spriteIndex] != null)
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else
			Debug.LogError("Sprite is missing!");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
