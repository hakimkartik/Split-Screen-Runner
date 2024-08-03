using System;
using UnityEngine.SceneManagement;

namespace PortKey.Assets.Script.SwitchLevel
{
    public class LevelInfo
    {
        private static LevelInfo _instance;
        public static LevelInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelInfo();
                }
                else
                {
                    _instance.RefreshLevel();
                }
                return _instance;
            }
        }

        public int Level { get; private set; }

        private LevelInfo()
        {
            RefreshLevel();
        }

        public void RefreshLevel()
        {
            string levelName = SceneManager.GetActiveScene().name;
        
            switch (levelName)
            {
                case "Tutorial3":
                    Level = -2;
                    break;
                case "Tutorial":
                case "Tutorial2":
                case "Tutorial4":
                    Level = 0;
                    break;
                case "Level1":
                    Level = 1;
                    break;
                case "Level2":
                    Level = 2;
                    break;
                case "Level3":
                    Level = 3;
                    break;
                case "Level4":
                    Level = 4;
                    break;
                case "Level5":
                    Level = 5;
                    break;
                case "Level6":
                    Level = 6;
                    break;
                case "Level7":
                    Level = 7;
                    break;
                case "Level8":
                    Level = 8;
                    break;
                case "Level8-Shoot":
                    Level = 8;
                    break;
                default:
                    Level = -1;
                    break;
            }
        }
    }
}
