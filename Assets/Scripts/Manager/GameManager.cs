using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class GameManager : MonoBehaviour
    {
        public static int Level // current level
        {
            get => _level;
            set
            {
                if (value <= 8)
                {
                    _level = value;
                }
                MaxLevel = Math.Max(_level, MaxLevel);
                PlayerPrefs.SetInt("MaxLevel", MaxLevel);
            }
        }

        private static int _level = 1;
        public static int MaxLevel = 1; // max level we get
        public static bool ControlFlag; // switch the player role
        public static int[] Diamonds = new int[9];
        public static int CurDiamonds;


        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (PlayerPrefs.GetInt("Initial", 0) == 0)
            {
                SceneManager.LoadScene("Help");
                PlayerPrefs.SetInt("Initial", 1);
            }
            for (int i = 0; i < 9; i++)
            {
                Diamonds[i] = PlayerPrefs.GetInt($"Diamond{i}", 0);
            }
        }

        public void OnGameLose()
        {
            SceneManager.LoadScene("Lose");
        }

        public void OnGameWin()
        {
            SceneManager.LoadScene("Win");
        }

        public void OnGameFinal()
        {
            SceneManager.LoadScene("Final");
        }
    }
}