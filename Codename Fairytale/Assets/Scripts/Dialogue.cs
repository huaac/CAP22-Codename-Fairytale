using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make this script into a data structure
[System.Serializable]
public class Dialogue
{
    //The name of the speaker
    public string name;

    //The dialogue which will be typed/inputted into dialogue space
    //Increased the text area to allow for more room in the inspector to write
    [TextArea(3, 10)]
    public string[] paragraphs;
}

