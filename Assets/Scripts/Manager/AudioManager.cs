using UnityEngine;

namespace DefaultNamespace
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        public AudioSource audioMove;
        public AudioSource audioRotate;
        public AudioSource audioThrowDown;
        public AudioSource audioJump;
        public AudioSource audioHurt;

        public void PlayHurtAudio()
        {
            audioHurt.Play();
        }

        public void PlayThrowDownAudio()
        {
            audioThrowDown.Play();
        }

        public void PlayMoveAudio()
        {
            audioMove.Play();
        }


        public void PlayRotateAudio()
        {
            audioRotate.Play();
        }
        
        public void PlayJumpAudio(){
            audioJump.Play();
        }
    }
}