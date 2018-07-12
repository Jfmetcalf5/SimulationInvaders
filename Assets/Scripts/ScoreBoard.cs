using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

  
    Text scoreText;
    int score;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
	
    public void scoreHit(int scoreIncrease) 
    {
        score += scoreIncrease;
        scoreText.text = score.ToString();
    }
}
