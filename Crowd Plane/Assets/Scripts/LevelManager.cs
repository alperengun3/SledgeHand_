using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Level currentLevel;

    private static LevelManager instance;
    private PlayerControl playerControl;

    private void Awake()
    {
        instance = this;
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        playerControl = PlayerControl.instance;
        CreateLevel();
    }

    private void Update()
    {
        //if (playerControl.lastChild != null && playerControl.lastChild.transform.position.y == 0.5f)
        //{
        //    SceneManager.LoadScene("level");
        //}
    }

    void CreateLevel()
    {
        currentLevel = Resources.Load("Levels/Level" + PlayerPrefs.GetInt("Level", 1)) as Level;

        if (currentLevel == null)
        {
            PlayerPrefs.SetInt("Level", 1);
            CreateLevel();
            return;
        }

        GameObject level = Instantiate(currentLevel.levelPrefab, Vector3.zero, Quaternion.identity);
    }
    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }

}
