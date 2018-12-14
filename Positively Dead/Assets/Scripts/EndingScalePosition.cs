using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Sam Heckle
/// Keeps the scale values from previous scene
/// </summary>
public class EndingScalePosition : MonoBehaviour {

	// objects for the scale movement
	public GameObject scaleArms;
	public GameObject rightBasketRed;
	public GameObject rightBasketBlue;
	public GameObject leftBasket;

	// Use this for initialization
	void Start () {
		
		int karma = PlayerPrefs.GetInt ("Karma", 1);
		
		if (karma < 0) {
			rightBasketRed.SetActive (false);
			rightBasketBlue.SetActive (true);
		}

		scaleArms.transform.rotation = new Quaternion (0, 0, PlayerPrefs.GetFloat("arms"), 1);
		leftBasket.transform.position = new Vector3 (leftBasket.transform.position.x, PlayerPrefs.GetFloat("leftBasket"), leftBasket.transform.position.z);

		if (rightBasketRed.activeInHierarchy == true)
			rightBasketRed.transform.position = new Vector3 (rightBasketRed.transform.position.x, PlayerPrefs.GetFloat("rightBasketRed"), rightBasketRed.transform.position.z);
		else
			rightBasketBlue.transform.position = new Vector3 (rightBasketBlue.transform.position.x, PlayerPrefs.GetFloat("rightBasketRed"), rightBasketBlue.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
