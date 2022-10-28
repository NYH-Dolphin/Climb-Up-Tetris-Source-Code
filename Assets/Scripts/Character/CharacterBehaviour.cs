using System;
using Tetris;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterBehaviour : MonoBehaviour
    {
        public float fMoveSpeed = 5;
        public float fJumpSpeed = 5;
        public ParticleSystem pJumpEffect;
        public Animator animator;
        private bool _bUpdate = false;

        public int iHP
        {
            get => _iHp;
            set
            {
                if (value > _iHp)
                {
                    if (value <= 3)
                    {
                        _iHp = value;
                        UIGamePage.Instance.ChangeHP(_iHp);
                    }
                }

                if (value >= 0 && value < _iHp)
                {
                    _iHp = value;
                    CollideWithBrick();
                    CameraBehaviour.Instance.OnShakeCamera(0.1f, 0.3f, 0.3f);
                    AudioManager.Instance.PlayHurtAudio();
                }
                else if(value < 0)
                {
                    GameManager.Instance.OnGameLose();
                }
            }
        }

        private int _iHp = 3;


        public static CharacterBehaviour Instance;

        private void Awake()
        {
            Instance = this;
        }

        private Rigidbody2D _rb;
        private bool _bIsJump;

        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
            pJumpEffect.Stop();
        }

        private void Update()
        {
            if (_bUpdate)
            {
                Escape();
                Move();
                Jump();
            }
        }

        public void OnStartUpdate()
        {
            _bUpdate = true;
            _rb.gravityScale = 2;
        }

        private void Escape()
        {
            if (!GameManager.ControlFlag)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    iHP -= 1;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    iHP -= 1;
                }
            }
        }


        public void CollideWithBrickDetect(int iBrick, int jBrick)
        {
            // Debug.Log($"iBrick={iBrick}, jBrick={jBrick}");
            Vector2 pos = transform.position;
            int i1 = (int)Math.Ceiling((pos.y - 4.75) / -0.5);
            int i2 = (int)Math.Floor((pos.y - 4.75) / -0.5);
            int j = (int)((pos.x + 2.25) / 0.5);
            // Debug.Log($"i1={i1}, j1={j}");
            // Debug.Log($"i2={i2}, j2={j}");
            if (iBrick == i1 && jBrick == j || (iBrick == i2 && jBrick == j))
            {
                iHP -= 1;
            }
        }


        private void CollideWithBrick()
        {
            Vector2 pos = transform.position;
            pos.y += 3;
            transform.position = pos;
            UIGamePage.Instance.ChangeHP(iHP);
        }

        private void Jump()
        {
            if (!GameManager.ControlFlag)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (!_bIsJump)
                    {
                        TetrisBehavior.Instance.CreateNextBrick();
                        _rb.velocity = new Vector2(_rb.velocity.x, fJumpSpeed);
                        _bIsJump = true;
                        AudioManager.Instance.PlayJumpAudio();
                        pJumpEffect.Play();
                        animator.Play("Jump");
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (!_bIsJump)
                    {
                        TetrisBehavior.Instance.CreateNextBrick();
                        _rb.velocity = new Vector2(_rb.velocity.x, fJumpSpeed);
                        _bIsJump = true;
                        AudioManager.Instance.PlayJumpAudio();
                        pJumpEffect.Play();
                        animator.Play("Jump");
                    }
                }
            }


            if (_rb.velocity.y == 0)
            {
                if (_bIsJump)
                {
                    _bIsJump = false;
                }
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 3 || other.gameObject.layer == 6)
            {
                if (_bIsJump)
                {
                    animator.Play("Hitground");
                }
            }
        }

        private void Move()
        {
            if (!GameManager.ControlFlag)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _rb.velocity = new Vector2(-fMoveSpeed, _rb.velocity.y);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    _rb.velocity = new Vector2(fMoveSpeed, _rb.velocity.y);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    _rb.velocity = new Vector2(-fMoveSpeed, _rb.velocity.y);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    _rb.velocity = new Vector2(fMoveSpeed, _rb.velocity.y);
                }
            }
        }
    }
}