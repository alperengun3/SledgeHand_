using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicLevel : MonoBehaviour
{
    InGameUI gameUI;
    [SerializeField] PlayerSettings settings;
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;
    [SerializeField] GameObject level4;

    void Start()
    {
        gameUI = FindObjectOfType<InGameUI>();

        if (settings.level == 0)
        {
            Level1();
        }

        if (settings.level == 1)
        {
            Level2();
        }

        if (settings.level == 2)
        {
            Level3();
        }

        if (settings.level == 3)
        {
            Level4();
        }

        if (settings.level == 4)
        {
            Level1();
        }

        if (settings.level == 5)
        {
            Level2();
        }

        if (settings.level == 6)
        {
            Level3();
        }

        if (settings.level == 7)
        {
            Level4();
        }

        if (settings.level == 8)
        {
            Level1();
        }

        if (settings.level == 9)
        {
            Level2();
        }

        if (settings.level == 10)
        {
            Level3();
        }

        if (settings.level == 11)
        {
            Level4();
        }

        if (settings.level == 12)
        {
            Level1();
        }

        if (settings.level == 13)
        {
            Level2();
        }

        if (settings.level == 14)
        {
            Level3();
        }

        if (settings.level == 15)
        {
            Level4();
        }

        if (settings.level == 16)
        {
            Level1();
        }

        if (settings.level == 17)
        {
            Level2();
        }

        if (settings.level == 18)
        {
            Level3();
        }

        if (settings.level == 19)
        {
            Level4();
        }
    }

    void Update()
    {
        
    }

    void Level1()
    {
        //Instantiate(level1);
        //Destroy(level4);
        level1.SetActive(true);
        level4.SetActive(false);
    }

    void Level2()
    {
        //Instantiate(level2);
        //Destroy(level1);
        level1.SetActive(false);
        level2.SetActive(true);
    }

    void Level3()
    {
        //Instantiate(level3);
        //Destroy(level2);
        level2.SetActive(false);
        level3.SetActive(true);
    }

    void Level4()
    {
        //Instantiate(level4);
        //Destroy(level3);
        level3.SetActive(false);
        level4.SetActive(true);
    }
}
