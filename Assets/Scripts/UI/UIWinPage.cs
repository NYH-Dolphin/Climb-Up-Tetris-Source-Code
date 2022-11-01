using System;
using Tetris;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIWinPage : MonoBehaviour
    {
        public void OnClickBackButton()
        {
            SceneManager.LoadScene("Main");
        }


        public void OnClickContinueButton()
        {
            SceneManager.LoadScene("Level" + GameManager.Level);
        }
    }
}