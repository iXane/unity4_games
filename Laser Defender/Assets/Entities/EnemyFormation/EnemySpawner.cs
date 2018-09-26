using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xmin;
	private float xmax;
	

	// Use this for initialization
	void Start () {
	
		// Getting the camera bounds to prevent the enemies moving out of the screen //
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 RightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		
		// Assinging the Edges //
		xmax = RightBoundary.x;
		xmin = leftBoundary.x;
	
		SpawnEnemies();
	}
	
	void SpawnEnemies()
	{	
		// Assign the position to each enemy child the same as the object (position) is on unity //
		foreach (Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		
		if (movingRight)
		{
			//..transform.position += new Vector3(speed * Time.deltaTime, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else
		{
			//transform.position += new Vector3(-speed * Time.deltaTime, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		
		if (leftEdgeOfFormation < xmin)
		{
			movingRight = true;
		
		}
		else if (rightEdgeOfFormation > xmax)
		{
			movingRight = false;
		}
		
		if (AllMembersDead())
		{
			SpawnUntilFull();
		}
		
	}
	
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		
		if (freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		
		if (NextFreePosition())
			Invoke ("SpawnUntilFull", spawnDelay);
	}
	
	Transform NextFreePosition()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
				return childPositionGameObject;				
		}
		
		return null;
	}
	
	bool AllMembersDead()
	{
		
		foreach(Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
				return false;				
		}
		
		return true;
	}
}
