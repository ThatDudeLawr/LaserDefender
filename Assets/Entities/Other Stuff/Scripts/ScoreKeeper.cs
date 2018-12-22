using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreKeeper : MonoBehaviour {

public static int score=0;
private Text myScore;
public void Score(int points){
	score += points;
	myScore.text = score.ToString();
}

void Start(){
	myScore = GetComponent<Text>();
	Reset();
}

public static void Reset(){
	score = 0;
}

}
