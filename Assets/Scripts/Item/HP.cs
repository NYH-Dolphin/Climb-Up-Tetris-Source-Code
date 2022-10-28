using UnityEngine;

namespace DefaultNamespace
{
    public class HP : MonoBehaviour
    {
        public GameObject objHP;

        public void OnTriggerEnter2D(Collider2D other)
        {
            GameObject.Find("Character").GetComponent<CharacterBehaviour>().iHP += 1;
            objHP.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}