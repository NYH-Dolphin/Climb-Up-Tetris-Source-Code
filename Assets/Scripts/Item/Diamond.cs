using Tetris;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public GameObject objCoin;
    public AudioSource effect;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character")
        {
            objCoin.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PlayerPrefs.SetInt($"level{GameManager.Level}", PlayerPrefs.GetInt($"level{GameManager.Level}", 0) + 1);
            effect.Play();
        }
    }
}