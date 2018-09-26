using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public static int ScoreAmount = 0;
	private Text scoreText;
	
	void Start()
	{
		scoreText = GetComponent<Text>();
		Reset();
	}
	
	public void AddPoints(int points)
	{
		ScoreAmount += points;
		scoreText.text = ScoreAmount.ToString();
	}
	
	public static void Reset()
	{
		ScoreAmount = 0;
	}
}
