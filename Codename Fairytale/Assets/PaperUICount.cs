using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperUICount : MonoBehaviour
{
    [SerializeField] private Text countText;
    [SerializeField] private GameObject papersGO;

    private int paperCount = 0;
    private List<Collectible> papers = new List<Collectible>();

    private void Awake()
    {
        foreach (Transform child in papersGO.transform)
        {
            Collectible paper = child.GetChild(0).GetComponent<Collectible>();
            paper.OnPaperCollected += IncrementCount;
            papers.Add(paper);
        }
    }

    private void OnDisable()
    {
        foreach (Collectible paper in papers)
        {
            paper.OnPaperCollected -= IncrementCount;
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
