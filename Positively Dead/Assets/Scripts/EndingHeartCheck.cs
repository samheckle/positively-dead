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

	private float timer;
	private int count;

	// karma number to show if the player is positive
	private int karma;

	// objects for the scale movement
	public GameObject scaleArms;
	public GameObject rightBasketRed;
	public GameObject rightBasketBlue;
	public GameObject leftBasket;

	// Use this for initialization
	void Start () {

		timer = 0;
		count = 0;

		rotate = scaleArms.transform.rotation.z;
		leftHeight = leftBasket.transform.position.y;
		PlayerPrefs.SetInt ("Karma", 1);
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

		timer += Time.deltaTime * 2;
		if (timer < 2) {
			Increase ();
		} else if (timer <= 6) {
			Decrease ();
		} else if (timer <= 7) {
			timer = 0;
			count++;
		}

		if (count == 2) {
			timer = 10;
		}
		if (timer == 10) {
			if (karma >= 0) {
				if (rotate <= .1)
					Increase ();
				else {
					PlayerPrefs.SetFloat ("arms", rotate);
					PlayerPrefs.SetFloat ("rightBasketRed", rightHeight);
					PlayerPrefs.SetFloat ("leftBasket", leftHeight);
					timer++;
				}
			} else {
				if (rotate >= -.1) {
					Decrease ();
				} else {
					PlayerPrefs.SetFloat ("arms", rotate);
					PlayerPrefs.SetFloat ("rightBasketRed", rightHeight);
					PlayerPrefs.SetFloat ("leftBasket", leftHeight);
					timer++;
				}
			}
		}

		if (timer == 11) {
			SceneManager.LoadScene ("EgyptMinigame3");
		}
	}

	/// <summary>
	/// For positive karma increases the heart scale side
	/// </summary>
	private void Increase () {
		rotate += Time.deltaTime / 10;
		leftHeight -= .01f;
		rightHeight += .009f;
		SetNewPosition ();

	}

	/// <summary>
	/// For negative karma, increases the feather scale side
	/// </summary>
	private void Decrease () {
		rotate -= Time.deltaTime / 10;
		leftHeight += .01f;
		rightHeight -= .009f;
		SetNewPosition ();

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