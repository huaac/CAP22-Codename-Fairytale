using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperUICount : MonoBehaviour
{
    [SerializeField] private Text countText;
    private int paperCount = 0;

    public void IncrementCount()
    {
        paperCount++;
        countText.text = paperCount.ToString();
    }
}
