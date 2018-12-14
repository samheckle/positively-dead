using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKPostEvent : MonoBehaviour {

	public void PlayButton()
	{
		AkSoundEngine.PostEvent("Button", gameObject);
	}

	public void PlayText()
	{
		AkSoundEngine.PostEvent("Type", gameObject);
	}
}
