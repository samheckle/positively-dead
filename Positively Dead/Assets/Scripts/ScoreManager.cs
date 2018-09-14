using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    Text scoreTxt;
    public static int score;

	// Use this for initialization
	void Start () {
        scoreTxt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreTxt.text = "Score: " + score;
	}
}
