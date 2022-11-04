using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //holds the dialogue information in inspector
    public Dialogue dialogue;

    //function which would send the information to the dialogue manager
    //currently attached to a button for testing purposes
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
