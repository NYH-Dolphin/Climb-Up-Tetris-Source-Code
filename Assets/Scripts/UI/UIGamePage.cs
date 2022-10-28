using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Tetris;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIGamePage : MonoBehaviour
    {
        public GameObject gamePage;
        public GameObject returnPage;

        public List<GameObject> nextBricks;
        public List<Toggle> listHP;

        public static UIGamePage Instance;


        private Dictionary<string, GameObject> dicNextBricks = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> _pages = new Dictionary<string, GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AddPages();
            OpenPage(gamePage.name);
        }

        public void ChangeHP(int hp)
        {
            for (int i = 0; i < listHP.Count; i++)
            {
                listHP[i].isOn = i + 1 <= hp;
            }
        }

        private void AddPages()
        {
            _pages.Add(gamePage.name, gamePage);
            _pages.Add(returnPage.name, returnPage);
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


        public void Initial()
        {
            foreach (GameObject brick in nextBricks)
            {
                dicNextBricks[brick.name] = brick;
                brick.SetActive(false);
            }
        }

        public void OnDisplayNextBrick(string brickNum)
        {
            foreach (GameObject brick in nextBricks)
            {
                
                brick.SetActive(false);
            }
            dicNextBricks[brickNum].SetActive(true);
            dicNextBricks[brickNum].transform.localScale = Vector3.one;
            StartCoroutine(Animation(dicNextBricks[brickNum]));
        }


        IEnumerator Animation(GameObject obj)
        {
            iTween.ScaleFrom(obj,
                iTween.Hash("scale", Vector3.zero, "time", 0.2f, "easetype", iTween.EaseType.easeOutBounce));
            yield break;
        }


        #region [GamePage]

        public void OnClickGamePageReturnButton()
        {
            Time.timeScale = 0;
            returnPage.SetActive(true);
        }

        #endregion


        #region [ReturnPage]

        public void OnClickReturnPageContinueButton()
        {
            Time.timeScale = 1;
            returnPage.SetActive(false);
        }

        public void OnClickReturnPageExitButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main");
        }

        #endregion
    }
}