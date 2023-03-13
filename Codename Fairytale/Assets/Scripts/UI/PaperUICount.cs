using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperUICount : MonoBehaviour
{
    [SerializeField] private Text countText;
    [SerializeField] private GameObject papersGO;

    private int paperCount = 0;
    private List<Paper> papers = new List<Paper>();

    private void Awake()
    {
        foreach (Transform child in papersGO.transform)
        {
            Paper paper = child.GetChild(0).GetComponent<Paper>();
            paper.OnCollected += IncrementCount;
            papers.Add(paper);
        }
    }

    private void OnDisable()
    {
        foreach (Paper paper in papers)
        {
            paper.OnCollected -= IncrementCount;
        }
    }

    public void IncrementCount()
    {
        paperCount++;
        countText.text = paperCount.ToString();
        PlayerPrefs.SetInt("paperCount", paperCount);
    }

    public void SetText() 
    {
        countText.text = PlayerPrefs.GetInt("paperCount").ToString();
    }
}
