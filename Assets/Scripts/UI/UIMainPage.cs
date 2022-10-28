using System.Collections.Generic;
using Tetris;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainPage : MonoBehaviour
{
    public GameObject homePage;
    public GameObject levelPage;
    public GameObject settingPage;


    private Dictionary<string, GameObject> _pages = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        AddPages();
        OpenPage(homePage.name);
        
        // Make player prefab delete all the keys
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void AddPages()
    {
        _pages.Add(homePage.name, homePage);
        _pages.Add(levelPage.name, levelPage);
        _pages.Add(settingPage.name, settingPage);
    }

    /// <summary>
    /// Open Specific Page According to the [Page Name]
    /// </summary>
    /// <param name="pageName"></param>
    private void OpenPage(string pageName)
    {
        foreach (GameObject page in _pages.Values)
        {
            page.SetActive(false);
        }

        _pages[pageName].SetActive(true);
    }


    #region [HomePage]

    public void OnClickHomePageSettingButton()
    {
        settingPage.SetActive(true);
    }

    public void OnClickHomePageHelpButton()
    {
        SceneManager.LoadScene("Help");
    }

    /// <summary>
    /// Start Game
    /// </summary>
    public void OnClickHomePageGoButton()
    {
        OpenPage(levelPage.name);
        // SceneManager.LoadScene("Game");
        // GameManager.Instance.OnStartGame();
    }

    #endregion


    #region [LevelPage]

    public void OnClickLevelPageBackButton()
    {
        OpenPage(homePage.name);
    }


    public void OnClickLevelButton(GameObject button)
    {
        SceneManager.LoadScene("Level" + button.name);
        GameManager.Level = int.Parse(button.name);
    }

    #endregion


    #region [SettingPage]

    public Toggle musicToggle;
    public Slider musicSlider;


    public void OnClickSettingPageExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public void OnClickSettingPageBackButton()
    {
        OpenPage(homePage.name);
    }

    #endregion

}