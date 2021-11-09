//using ElephantSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Text levelText;
    public GameObject levelCompletePanel;
    public GameObject levelFailPanel;
    public GameObject tutorialPanel;
    public GameObject tapToStartPanel;
    public GameObject confettiRain;
    public GameObject pauseButton;
    public GameObject levelIndicator;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject progressIndicator;
    PlayerControl player;
    CamControl cam;
    [SerializeField] PlayerSettings settings;
    [HideInInspector] public bool levelFinished;
    [HideInInspector] public bool levelStarted;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        cam = FindObjectOfType<CamControl>();

        confettiRain.SetActive(false);

        if (SaveLoadManager.getLastPlayedLevel() != SceneManager.GetActiveScene().buildIndex)
        {
            //SceneManager.LoadScene(SaveLoadManager.getLastPlayedLevel());
        }
        levelText.text = "Level " + (settings.level + 1).ToString();

        //Elephant.LevelStarted(SaveLoadManager.getFakeLevel());

        //tapToStartPanel.SetActive(true);

        if (SaveLoadManager.getFakeLevel() != 1)
        {
            //
        }
        else
        {
            //tutorialPanel.SetActive(true);
        }
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void StartLevel()
    {
        levelStarted = true;
        tutorialPanel.SetActive(false);
        tapToStartPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadNextLevel()
    {
        //LevelName();
        //cam.targetPos = cam.boxer;
        //cam.transform.position = new Vector3(2.14f, 5, -7.28f);
        //player.transform.position = new Vector3(0, 0.5f, 0);

        //LevelManager.Instance.Invoke("LoadNextLevel", 0.25f);

        //SceneManager.LoadScene("SampleScene");

        settings.level++;

        if (settings.level == 20)
        {
            settings.level = 0;
        }

        SaveLoadManager.increaseFakeLevel();
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            int randomLevel = Random.Range(2, 4);
            SaveLoadManager.setLastPlayedLevel(1);
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
            //SaveLoadManager.setLastPlayedLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public IEnumerator levelComplete()
    {
        //Elephant.LevelCompleted(SaveLoadManager.getFakeLevel());

        levelFinished = true;
        yield return new WaitForSeconds(0.2f);
        pauseButton.SetActive(false);
        levelIndicator.SetActive(false);
        progressIndicator.SetActive(false);
        levelCompletePanel.SetActive(true);
        confettiRain.SetActive(true);
    }

    public IEnumerator levelFail()
    {
        //Elephant.LevelFailed(SaveLoadManager.getFakeLevel());

        levelFinished = true;
        yield return new WaitForSeconds(0.2f);
        pauseButton.SetActive(false);
        progressIndicator.SetActive(false);
        levelIndicator.SetActive(false);
        levelFailPanel.SetActive(true);
    }

    //void LevelName()
    //{
    //    if (level2 != null)
    //    {
    //        level1 = GameObject.FindGameObjectWithTag("level" + SaveLoadManager.getFakeLevel().ToString());
    //        level2 = GameObject.FindGameObjectWithTag("level" + SaveLoadManager.getFakeLevel().ToString() + 1);

    //        level1.SetActive(false);
    //        level2.SetActive(true);
    //    }
    //    else
    //    {
    //        level2 = GameObject.FindGameObjectWithTag("level" + 1);

    //        level1.SetActive(false);
    //        level2.SetActive(true);
    //    }
    //}
}
