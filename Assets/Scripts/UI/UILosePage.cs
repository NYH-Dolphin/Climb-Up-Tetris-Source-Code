using Tetris;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UILosePage : MonoBehaviour
    {
        public void OnClickRestartButton()
        {
            SceneManager.LoadScene("Level" + GameManager.Level);
        }
        
        public void OnClickBackButton()
        {
            SceneManager.LoadScene("Main");
        }

    }
}