

using Tetris;
using UnityEngine;
using Utils;

public class Eye : MonoBehaviour
{
    public float fLastTime = 10;
    private TimeCountDown _countDown;
    private bool _bTriggered;
    private BoxCollider2D _collider2D;
    public GameObject objEye;


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
            GameObject.Find("Character").transform.Find("CharacterPortrait").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Character").transform.Find("JumpEffect").gameObject.SetActive(false);
            _collider2D.enabled = false;
            objEye.SetActive(false);
        }
    }


    private void Update()
    {
        if (_bTriggered)
        {
            _countDown.Tick(Time.deltaTime);
            if (_countDown.TimeOut)
            {
                GameObject.Find("Character").transform.Find("CharacterPortrait").GetComponent<SpriteRenderer>().enabled = true;
                GameObject.Find("Character").transform.Find("JumpEffect").gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
    
    
    
}