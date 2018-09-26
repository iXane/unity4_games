using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text lblScore = GetComponent<Text>();
		lblScore.text = Score.ScoreAmount.ToString();
		Score.Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
