using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Author: Nikolas Whiteside
/// Animates text on the hud like a typewriter
/// </summary>
public class Typewriter : MonoBehaviour
{
    //The text to be animated
    private Text animText;

    private IEnumerator coroutine;

    private AKPostEvent akPostScript;

    //Test to see if the string has changed
    private string testText;

    //The final string to display
    public string finalText;    

    //The typing delay
    public float typeDelay;

    public float currentDelay;

    public bool animComplete;
    private bool coroutineComplete;

    // Use this for initialization
    void Awake()
    {
        currentDelay = typeDelay;
        animText = gameObject.GetComponent<Text>();
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        //Check whether the string has changed
        if (finalText != testText)
        {
            //Reset the text and begin the animation again
            StopAllCoroutines();
            coroutine = Type();
            animText.text = "";
            StartCoroutine(coroutine);
        }

        if (Input.GetMouseButton(0))
        {
            currentDelay = 0f;
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary)
            {
                currentDelay = 0f;
            }
        }
        else
        {
            currentDelay = typeDelay;
            if(coroutineComplete)
            {
                animComplete = true;
            }
        }

        //Set the test equal to the desired string
        testText = finalText;
    }

    /// <summary>
    /// Types out the text
    /// </summary>
    IEnumerator Type()
    {
        animComplete = false;
        coroutineComplete = false;
        for (int i = 0; i < finalText.Length + 1; i++)
        {
            akPostScript = GameObject.Find("Music").GetComponent<AKPostEvent>();
            akPostScript.PlayText();

            animText.text = finalText.Substring(0, i);
            yield return new WaitForSeconds(currentDelay);
        }
        coroutineComplete = true;
    }
}