using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make this script into a scriptable objecy which can be added through menu
[CreateAssetMenu(fileName = "dialogue", menuName = "CreateDialogue")]
public class Dialogue : ScriptableObject
{
    //The dialogue which will be typed/inputted into dialogue space
    //Increased the text area to allow for more room in the inspector to write
    [TextArea(3, 10)]
    public string[] paragraphs;
}

