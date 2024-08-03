using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelsManager : MonoBehaviour
{
    //public static GameLevelsManager Instance { get; private set; }

    public static GameLevelsManager Instance;

    public int Level { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}