using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgmt : MonoBehaviour
{
    [SerializeField] private float levelRestartDelay;
    [SerializeField] private int restartCount;

    //Loads Chapter Select
    public void ChapterSelect() 
    {
        flipPage();
        //SceneManager.LoadScene(1);
    }
    
    //Loads the first level
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    // Resets current level
    public void ResetScene()
    {
        StartCoroutine(ResetAfterDelay(levelRestartDelay));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IncrementRestartCount();
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadNextLevel()
    {
        ResetRestartCount();
        GameManager.Instance.completedLevels += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel);
        }
    }

    //Quits the game. Only works when it's played as a game,
    //not in the Unity Client. It'll print out "QUIT!" in the log.
    public void QuitGame()
    {
        ResetRestartCount();
        Debug.Log("QUIT!");
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        Application.Quit();
    }

    // enable and disable menu screen. freeze game while in menu.
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }

    //Switches to the Title Screen/Menu Screen
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void IncrementRestartCount()
    {
        restartCount += 1;
    }
    private void ResetRestartCount()
    {
        restartCount = 0;
    }

    private void flipPage()
    {
        Debug.Log("hello");
        this.transform.GetChild(2).transform.gameObject.SetActive(true);
        this.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        StartCoroutine("timeforPageFlip");
    }

    private IEnumerator timeforPageFlip()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(1);
    }
}
