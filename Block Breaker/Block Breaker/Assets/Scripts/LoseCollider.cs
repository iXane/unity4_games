﻿using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;

	void OnTriggerEnter2D(Collider2D collider)
	{
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		//Brick.breakableCount = 0;
		levelManager.LoadLevel("Lose");
	}
	
	void OnCollisionEnter2D(Collision2D collission)
	{
	
	}
}