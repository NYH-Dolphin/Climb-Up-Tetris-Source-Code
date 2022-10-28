using System;
using Tetris;
using UnityEngine;
using Utils;

public class Clock : MonoBehaviour
{
    public float fLastTime = 5;
    private TimeCountDown _countDown;
    private bool _bTriggered;
    private BoxCollider2D _collider2D;
    public GameObject objClock;


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
            TetrisBehavior.Instance.curBrick.GetComponent<BrickBehavior>().ChangeMoveDownTime(3f);
            _collider2D.enabled = false;
            objClock.SetActive(false);
        }
    }


    private void Update()
    {
        if (_bTriggered)
        {
            _countDown.Tick(Time.deltaTime);
            if (_countDown.TimeOut)
            {
                TetrisBehavior.Instance.curBrick.GetComponent<BrickBehavior>().ChangeMoveDownTime(1f);
                gameObject.SetActive(false);
            }
        }
    }
}