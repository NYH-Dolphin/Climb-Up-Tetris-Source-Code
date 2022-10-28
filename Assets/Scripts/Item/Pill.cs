using System;
using Tetris;
using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class Pill : MonoBehaviour
    {
        public float fLastTime = 10;
        private TimeCountDown _countDown;
        private bool _bTriggered;
        private BoxCollider2D _collider2D;
        public GameObject objPill;

        private void Start()
        {
            _countDown = new TimeCountDown(fLastTime);
            _collider2D = gameObject.GetComponent<BoxCollider2D>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Character")
            {
                _bTriggered = true;
                other.GetComponent<CharacterBehaviour>().fJumpSpeed = 20;
                _collider2D.enabled = false;
                objPill.SetActive(false);
            }
        }


        private void Update()
        {
            if (_bTriggered)
            {
                _countDown.Tick(Time.deltaTime);
                if (_countDown.TimeOut)
                {
                    GameObject.Find("Character").GetComponent<CharacterBehaviour>().fJumpSpeed = 8;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}