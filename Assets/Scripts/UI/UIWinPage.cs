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
    }
}