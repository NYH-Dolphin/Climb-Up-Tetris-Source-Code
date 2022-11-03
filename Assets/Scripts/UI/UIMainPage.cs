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
    public List<GameObject> levelButton;


    private Dictionary<string, GameObject> _pages = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        AddPages();
        OpenPage(homePage.name);
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
        OnOpenLevelPage();
    }

    #endregion


    #region [LevelPage]

    public void OnClickLevelPageBackButton()
    {
        OpenPage(homePage.name);
    }


    public void OnClickLevelButton(GameObject button)
    {
        GameManager.Level = int.Parse(button.name);
        SceneManager.LoadScene("Level" + button.name);
    }

    public void OnOpenLevelPage()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject button = levelButton[i].gameObject;
            if (int.Parse(button.name) > GameManager.MaxLevel)
            {
                button.GetComponent<Button>().enabled = false;
                button.transform.GetChild(0).GetComponent<Text>().text = "??";
                button.transform.GetChild(1).GetComponent<Text>().text = "??";
            }
            else
            {
                button.GetComponent<Button>().enabled = true;
                button.transform.GetChild(0).GetComponent<Text>().text = button.name;
                button.transform.GetChild(1).GetComponent<Text>().text = button.name;
            }
        }
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

    public void OnClickSettingPageCleanCacheButton()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion

}