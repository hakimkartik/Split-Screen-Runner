using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortKey.Assets.Script
{
    public static class Level8Info
    {
        private static bool _scoreUp = false;
        private static bool _ctrlFlip = false;
        private static bool _lives = false;
        private static bool _antiHealth = false;
        private static bool _turtle = false;
        private static bool _shooting = false;

        public static bool GetScoreUp()
        {
            return _scoreUp;
        }

        public static void SetScoreUp(bool value)
        {
            _scoreUp = value;
        }

        public static bool GetCtrlFlip()
        {
            return _ctrlFlip;
        }

        public static void SetCtrlFlip(bool value)
        {
            _ctrlFlip = value;
        }

        public static bool GetLives()
        {
            return _lives;
        }

        public static void SetLives(bool value)
        {
            _lives = value;
        }

        public static bool GetAntiHealth()
        {
            return _antiHealth;
        }

        public static void SetAntiHealth(bool value)
        {
            _antiHealth = value;
        }

        public static bool GetTurtle()
        {
            return _turtle;
        }

        public static void SetTurtle(bool value)
        {
            _turtle = value;
        }

        public static bool GetShooting()
        {
            return _shooting;
        }

        public static void SetShooting(bool value)
        {
            _shooting = value;
        }
    }
}
