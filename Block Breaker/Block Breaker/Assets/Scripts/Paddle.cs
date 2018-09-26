using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public bool autoPlay = false;
	private ball bola;
	

	// Use this for initialization
	void Start () {
		bola = GameObject.FindObjectOfType<ball>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!autoPlay)
			MoveWithMouse();
		else
		{
			AutoPlay();
		}
	}
	
	void AutoPlay()
	{
		Vector3 paddlePOS = new Vector3(0.5f, this.transform.position.y, 0f);
		
		Vector3 ballPosition = bola.transform.position;
		
		paddlePOS.x = Mathf.Clamp(ballPosition.x, 0.5f, 15.5f);
		
		this.transform.position = paddlePOS;
	}
	
	void MoveWithMouse()
	{
		Vector3 paddlePOS = new Vector3(0.5f, this.transform.position.y, 0f);
		
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		
		paddlePOS.x = Mathf.Clamp(mousePosInBlocks, 0.5f, 15.5f);
		
		this.transform.position = paddlePOS;
	}
}
