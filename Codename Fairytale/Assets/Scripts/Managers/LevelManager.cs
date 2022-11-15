using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    //private static LevelManager instance = new LevelManager();
    //LevelManager();

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject lm = new GameObject("LevelManager");
                lm.AddComponent<LevelManager>();
            }
            return instance;
        }
    }

    int levelsUnlocked = 1;
    public Button[] buttons;

    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    //check to make sure the levelIndex and Scene numbers line up properly!
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex + 1);
    }
}
