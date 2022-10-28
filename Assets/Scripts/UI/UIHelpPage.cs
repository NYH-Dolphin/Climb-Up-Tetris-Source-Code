using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHelpPage : MonoBehaviour
{

    public void OnClickHomePageBackButton()
    {
        SceneManager.LoadScene("Main");
    }
}