using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPostEvent : MonoBehaviour {

    public void PlayButtonClick() 
    {
        AkSoundEngine.PostEvent("Button", gameObject);
    }
}
