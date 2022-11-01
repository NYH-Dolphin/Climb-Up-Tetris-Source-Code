﻿using System;
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
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void OnGameLose()
        {
            SceneManager.LoadScene("Lose");
        }

        public void OnGameWin()
        {
            SceneManager.LoadScene("Win");
        }
    }
}