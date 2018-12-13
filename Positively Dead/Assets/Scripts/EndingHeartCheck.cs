using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Sam Heckle
/// Moves the scale based on whether the player has positive or negative karma
/// </summary>
public class EndingHeartCheck : MonoBehaviour {

	// private variables to adjust values of the scale
	private float rotate;
	private float leftHeight;
	private float rightHeight;

	// karma number to show if the player is positive
	private int karma;

	// objects for the scale movement
	public GameObject scaleArms;
	public GameObject rightBasketRed;
	public GameObject rightBasketBlue;
	public GameObject leftBasket;

	// Use this for initialization
	void Start () {

		rotate = scaleArms.transform.rotation.z;
		leftHeight = leftBasket.transform.position.y;
		karma = PlayerPrefs.GetInt ("Karma", 1);

		if (karma < 0) {
			rightBasketRed.SetActive (false);
			rightBasketBlue.SetActive (true);
		}
		if (rightBasketRed.activeInHierarchy == true)
			rightHeight = rightBasketRed.transform.position.y;
		else
			rightHeight = rightBasketBlue.transform.position.y;

	}

	// Update is called once per frame
	void Update () {
		if (karma >= 0) {
			Increase ();
		} else {
			Decrease ();
		}

	}

	/// <summary>
	/// For positive karma increases the heart scale side
	/// </summary>
	private void Increase () {
		if (rotate <= .1) {
			rotate += Time.deltaTime / 10;
			leftHeight -= Time.deltaTime / 1.6f;
			rightHeight += Time.deltaTime / 2f;
			SetNewPosition ();
		}

	}

	/// <summary>
	/// For negative karma, increases the feather scale side
	/// </summary>
	private void Decrease () {
		if (rotate >= -.1) {
			rotate -= Time.deltaTime / 10;
			leftHeight += Time.deltaTime / 2f;
			rightHeight -= Time.deltaTime / 1.6f;
			SetNewPosition ();
		}
	}

	/// <summary>
	/// Helper method to move the scale componenets
	/// </summary>
	private void SetNewPosition () {
		scaleArms.transform.rotation = new Quaternion (0, 0, rotate, 1);
		leftBasket.transform.position = new Vector3 (leftBasket.transform.position.x, leftHeight, leftBasket.transform.position.z);

		if (rightBasketRed.activeInHierarchy == true)
			rightBasketRed.transform.position = new Vector3 (rightBasketRed.transform.position.x, rightHeight, rightBasketRed.transform.position.z);
		else
			rightBasketBlue.transform.position = new Vector3 (rightBasketBlue.transform.position.x, rightHeight, rightBasketBlue.transform.position.z);
	}
}