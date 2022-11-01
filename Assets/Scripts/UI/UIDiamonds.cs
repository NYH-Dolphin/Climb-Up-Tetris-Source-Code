using System.Collections.Generic;
using Tetris;
using UnityEngine;

namespace UI
{
    public class UIDiamonds : MonoBehaviour
    {
        public List<GameObject> Diamonds;
        private void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                Diamonds[i].transform.GetChild(0).gameObject.SetActive(GameManager.Diamonds[int.Parse(gameObject.name)] >= i + 1);
            }
        }
    }
}