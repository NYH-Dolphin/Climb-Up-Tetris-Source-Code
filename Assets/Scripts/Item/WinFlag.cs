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
                GameManager.Diamonds[GameManager.Level] =
                    Mathf.Max(GameManager.Diamonds[GameManager.Level], GameManager.CurDiamonds);
                PlayerPrefs.SetInt($"Diamond{GameManager.Level}", GameManager.Diamonds[GameManager.Level]);
                GameManager.CurDiamonds = 0;
                GameManager.Level += 1;
                GameManager.Instance.OnGameWin();
            }
        }
    }
}