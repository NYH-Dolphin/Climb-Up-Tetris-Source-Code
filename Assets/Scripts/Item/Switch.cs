using System;
using Tetris;
using UnityEngine;

namespace DefaultNamespace
{
    public class Switch : MonoBehaviour
    {
        public GameObject objSwitch;
        private BoxCollider2D _collider;

        private void Start()
        {
            _collider = transform.GetComponent<BoxCollider2D>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Character")
            {
                objSwitch.SetActive(false);
                GameManager.ControlFlag = !GameManager.ControlFlag;
                _collider.enabled = false;
            }
        }
    }
}