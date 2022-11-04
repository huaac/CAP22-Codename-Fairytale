using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //The list of the dialogue that needs to be gone through
    private Queue<string> piecesOfDialogue;

    //Connects to the physical dialoguebox
    public Text speakerName;
    public Text dialogueText;

    public GameObject dialogueBox;

    [SerializeField] private float typewriterSpeed = 0.15f;

    // Creates an empty dialogue queue
    void Start()
    {
        piecesOfDialogue = new Queue<string>();
    }

    //Called when a new instance of dialogue is happening
    //Clears the old queue of any possible information left & Sets the name of the speaker
    //Places the new information and paragraphs/sentences into the queue and then calls ReadSentence
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        speakerName.text = dialogue.name;

        piecesOfDialogue.Clear();

        foreach(string excerpt in dialogue.paragraphs)
        {
            piecesOfDialogue.Enqueue(excerpt);
        }

        ReadSentence();
    }

    //ReadSentence first checks to see if the dialogue Queue is empty. If its empty, it ends the dialogue
    //Otherwise it will dequeu the next sentence/paragraph in the queue and put it in a string
    //The string then will be sent to the dialogue text of the physical dialoguebox
    //This will be the function called most often to advance dialogue
    public void ReadSentence()
    {
        if(piecesOfDialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string currentExcerpt = piecesOfDialogue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(WriteSentence(currentExcerpt));
        //dialogueText.text = currentExcerpt;
    }

    //Just a nifty function that makes it look like the words are being written
    //Not necessary but cool
    IEnumerator WriteSentence(string currentExcerpt)
    {
        dialogueText.text = "";
        foreach(char letter in currentExcerpt.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typewriterSpeed);
        }
    }
    
    //Ends the current dialogue instance
    private void EndDialogue()
    {
        Debug.Log("Done");
        dialogueBox.SetActive(false);
    }
}
