using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PortKey.Assets.Script;

//code from https://www.youtube.com/watch?v=PpIkrff7bKU

public class SceneSwitch : MonoBehaviour
{
    //int MAP_SCENE_BUILD_NUMBER = 7;

    //LEVELS
    public void loaderLvl1()
    {
        if (!TutorialInfo.lvl1)
        {
            loaderTut();
        }
        else
        {
            TutorialInfo.lastScene = 1;
            GameLevelsManager.Instance.Level = 1;
            SceneManager.LoadScene("Level1");
        }
    }
    public void loaderLvl2()
    {
        TutorialInfo.lastScene = 2;
        GameLevelsManager.Instance.Level = 2;
        SceneManager.LoadScene("Level2");
    }
    public void loaderLvl3()
    {
        if (!TutorialInfo.lvl3)
        {
            loaderTut2();
        }
        else
        {
            TutorialInfo.lastScene = 3;
            GameLevelsManager.Instance.Level = 3;
            SceneManager.LoadScene("Level3");
        }
    }
    public void loaderLvl4()
    {
        TutorialInfo.lastScene = 4;
        GameLevelsManager.Instance.Level = 4;
        SceneManager.LoadScene("Level4");
    }
    public void loaderLvl5()
    {
        if (!TutorialInfo.lvl5)
        {
            loaderTut3();
        }
        else
        {
            TutorialInfo.lastScene = 5;
            GameLevelsManager.Instance.Level = 5;
            SceneManager.LoadScene("Level5");
        }
    }
    public void loaderLvl6()
    {
        TutorialInfo.lastScene = 6;
        GameLevelsManager.Instance.Level = 6;
        SceneManager.LoadScene("Level6");
    }
    public void loaderLvl7()
    {
        if (!TutorialInfo.lvl7)
        {
            loaderTut4();
        }
        else
        {
            TutorialInfo.lastScene = 7;
            GameLevelsManager.Instance.Level = 7;
            SceneManager.LoadScene("Level7");
        }
    }
    public void loaderCustomization()
    {
        TutorialInfo.lastScene = 108;
        SceneManager.LoadScene("Customization");
    }
    public void loaderLvl8()
    {
        TutorialInfo.lastScene = 8;
        GameLevelsManager.Instance.Level = 8;
        if (Level8Info.GetShooting())
        {
            SceneManager.LoadScene("Level8-Shoot");
        }
        else
        {
            SceneManager.LoadScene("Level8");
        }
    }

    //TUTORIALS
    public void loaderTut()
    {
        TutorialInfo.lastScene = -1;
        SceneManager.LoadScene("Tutorial");
        TutorialInfo.lvl1 = true;
    }
    public void loaderTut2()
    {
        TutorialInfo.lastScene = -2;
        SceneManager.LoadScene("Tutorial2");
        TutorialInfo.lvl3 = true;
    }
    public void loaderTut3()
    {
        TutorialInfo.lastScene = -3;
        SceneManager.LoadScene("Tutorial3");
        TutorialInfo.lvl5 = true;
    }
    public void loaderTut4()
    {
        TutorialInfo.lastScene = -4;
        SceneManager.LoadScene("Tutorial4");
        TutorialInfo.lvl7 = true;
    }

    //MENUS
    public void loaderMenu()
    {
        TutorialInfo.lastScene = 100;
        SceneManager.LoadScene("Menu");
    }
    public void loaderMenuTut()
    {
        TutorialInfo.lastScene = 101;
        SceneManager.LoadScene("TutorialMenu");
    }
    public void loaderStarter()
    {
        TutorialInfo.lastScene = 100;
        SceneManager.LoadScene("Starter");
    }
    public void loadMap()
    {
        SceneManager.LoadScene("Map");
    }

    //BACK BUTTON
    public void BackButton()
    {
        switch (TutorialInfo.lastScene)
        {
            case 1:
                loaderLvl1();
                break;
            case 2:
                loaderLvl2();
                break;
            case 3:
                loaderLvl3();
                break;
            case 4:
                loaderLvl4();
                break;
            case 5:
                loaderLvl5();
                break;
            case 6:
                loaderLvl6();
                break;
            case 7:
                loaderLvl7();
                break;
            case 8:
                loaderLvl8();
                break;
            case -1:
                loaderTut();
                break;
            case -2:
                loaderTut2();
                break;
            case -3:
                loaderTut3();
                break;
            case -4:
                loaderTut4();
                break;
            case 101:
                loaderMenuTut();
                break;
            case 108:
                loaderCustomization();
                break;
            case 100:
            default:
                loaderMenu();
                break;
        }
    }

}