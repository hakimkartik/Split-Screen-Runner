using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using PortKey.Assets.Script;

public class Lvl8Customization : MonoBehaviour
{
    public Toggle[] options;
    public Button startBtn;
    private SceneSwitch sceneSwitch;

    void Start()
    {
        startBtn.onClick.AddListener(OnSubmit);
        sceneSwitch = FindObjectOfType<SceneSwitch>();
    }

    void OnSubmit()
    {
        Level8Info.SetScoreUp(false);
        Level8Info.SetCtrlFlip(false);
        Level8Info.SetLives(false);
        Level8Info.SetAntiHealth(false);
        Level8Info.SetTurtle(false);
        Level8Info.SetShooting(false);

        if (options.Length > 0 && options[0].isOn)
        {
            Level8Info.SetScoreUp(true);
        }
        if (options.Length > 1 && options[1].isOn)
        {
            Level8Info.SetCtrlFlip(true);
        }
        if (options.Length > 2 && options[2].isOn)
        {
            Level8Info.SetLives(true);
        }
        if (options.Length > 3 && options[3].isOn)
        {
            Level8Info.SetAntiHealth(true);
        }
        if (options.Length > 4 && options[4].isOn)
        {
            Level8Info.SetTurtle(true);
        }
        if (options.Length > 5 && options[5].isOn)
        {
            Level8Info.SetShooting(true);
        }

        sceneSwitch.loaderLvl8();

        //Debug.Log("Level8Info Updated:");
        //Debug.Log("scoreUp: " + Level8Info.scoreUp);
        //Debug.Log("cntrFlip: " + Level8Info.cntrFlip);
        //Debug.Log("lives: " + Level8Info.lives);
        //Debug.Log("antiHealth: " + Level8Info.antiHealth);
        //Debug.Log("turtle: " + Level8Info.turtle);
        //Debug.Log("shooting: " + Level8Info.shooting);
    }
}