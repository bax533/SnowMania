using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

    public static bool endLevel = false;
    public Animator panelAnim;
    public GameObject pausePanel;
    public GameObject gameInputPanel;
    public Animator transAnim;


    public void LoadMenu()
    {
        StartCoroutine(LoadLevelRoutine(0));
        Time.timeScale = 1;
    }

    public void LoadByName(string lvlName)
    {
        endLevel = false;
        StartCoroutine(LoadLevelRoutine(lvlName));
    }

    public void FinishTutorialClick()
    {
        endLevel = false;
        PlayerPrefs.SetInt("Tutorial", 27);
        StartCoroutine(LoadLevelRoutine(0));
    }

    public void LoadLevel(int nr)
    {
        endLevel = false;
        StartCoroutine(LoadLevelRoutine("LEVEL" + nr.ToString()));
    }

    public void Restart()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevelRoutine(sceneIndex));
        Time.timeScale = 1;
    }

    public void Crash()
    {
        panelAnim.SetTrigger("crash");
        Values.END = true;
        Values.state = Values.State.Ground;
    }

    public void EndLevel()
    {
        gameInputPanel.SetActive(false);
        panelAnim.SetTrigger("gameover");
        score_Script.EndLVL();
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        //Values.PAUSE = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        //Values.PAUSE = false;
        Time.timeScale = 1;
    }

    IEnumerator LoadLevelRoutine(int levelBuildIndex)
    {
        transAnim.SetTrigger("endlvl");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelBuildIndex);
    }

    IEnumerator LoadLevelRoutine(string sceneName)
    {
        transAnim.SetTrigger("endlvl");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
