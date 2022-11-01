using System;
using Tetris;
using UnityEngine;

namespace DefaultNamespace
{
    public class WinFlag : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Character")
            {
                GameManager.Instance.OnGameWin();
                GameManager.Level += 1;
                GameManager.MaxLevel = Math.Max(GameManager.MaxLevel, GameManager.Level);
            }
        }
    }
}