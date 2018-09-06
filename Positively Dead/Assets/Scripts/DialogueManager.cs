using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Dialogue currentDialogue;
    private List<Dialogue> dialogues;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Text dialogueBox;

    [SerializeField]
    private Text speakerName;

	// Use this for initialization
	void Start () {
		Debug.Log("scene has loaded");
        /* 
         * Open file based on scene name
         * Parse through for character name, text and speed
         * Save each block as it's own dialogue object in the dictionary
         * FOR SAKE OF DEMO - kickstart dialogue runthrough
         */
        dialogues = new List<Dialogue>();

        dialogues.Add(new Dialogue("Nik", "Yo, what's up?"));
        dialogues.Add(new Dialogue("Sam", "*Mutters something sarcastic under breath*"));
        dialogues.Add(new Dialogue("Nik", "*Leaves and goes to Target where he lives*"));

        Debug.Log(dialogues[0]);
        dialogueBox.text = dialogues[0].Text;
        speakerName.text = dialogues[0].SpeakerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
