using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;


namespace Tetris
{
    public class BrickBehavior : MonoBehaviour
    {
        public int brickType;
        private int[,] _brick;
        public bool bFinish;
        private static int _dropRow = 0;
        private static int _dropCol = 4;

        private GameObject _gridPrefab;
        private List<GameObject> _gridList = new List<GameObject>();
        private int _curRow;
        private int _curCol;
        private TimeCountDown _moveDownCount = new TimeCountDown(1f);
        private TimeCountDown _leftRightCount = new TimeCountDown(0.1f);
        private TimeCountDown _waitCount = new TimeCountDown(0.3f);


        public void ChangeMoveDownTime(float timeValue)
        {
            _moveDownCount.Value = timeValue;
            _moveDownCount.FillTime();
        }


        private void Start()
        {
            _gridPrefab = Resources.Load($"Prefab/Bricks/grid {Random.Range(0, 7).ToString()}") as GameObject;
            // load data about certain brick type
            switch (brickType)
            {
                case 1:
                    _brick = TetrisBehavior.b1;
                    break;
                case 2:
                    _brick = TetrisBehavior.b2;
                    break;
                case 3:
                    _brick = TetrisBehavior.b3;
                    break;
                case 4:
                    _brick = TetrisBehavior.b4;
                    break;
                case 5:
                    _brick = TetrisBehavior.b5;
                    break;
                case 6:
                    _brick = TetrisBehavior.b6;
                    break;
                case 7:
                    _brick = TetrisBehavior.b7;
                    break;
            }

            // initial current row and column
            _curRow = _dropRow;
            _curCol = _dropCol;

            // initiate block
            for (int row = 0; row < _brick.GetLength(0); row++)
            {
                for (int col = 0; col < _brick.GetLength(1); col++)
                {
                    if (_brick[row, col] == 1)
                    {
                        GameObject grid = Instantiate(_gridPrefab, gameObject.transform, true);
                        Vector3 pos = TetrisBehavior._boardPos[_dropRow + row, _dropCol + col];
                        grid.transform.position = pos;
                        _gridList.Add(grid);
                    }
                }
            }

            // if initial collide, the game will be over
            if (IsCollide(_curRow, _curCol))
            {
                Debug.Log("aa");
                GameManager.Instance.OnGameLose();
            }
        }


        private void Update()
        {
            if (!bFinish)
            {
                if (!GameManager.ControlFlag)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Rotate();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Move(true);
                        _waitCount.FillTime();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Move(false);
                        _waitCount.FillTime();
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        MoveDown();
                        _moveDownCount.Value = 0.1f;
                        _moveDownCount.FillTime();
                    }

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        _waitCount.Tick(Time.deltaTime);
                        if (_waitCount.TimeOut)
                        {
                            _leftRightCount.Tick(Time.deltaTime);
                            if (_leftRightCount.TimeOut)
                            {
                                Move(true);
                                _leftRightCount.FillTime();
                            }
                        }
                    }

                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        _waitCount.Tick(Time.deltaTime);
                        if (_waitCount.TimeOut)
                        {
                            _leftRightCount.Tick(Time.deltaTime);
                            if (_leftRightCount.TimeOut)
                            {
                                Move(false);
                                _leftRightCount.FillTime();
                            }
                        }
                    }

                    if (Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        _moveDownCount.Value = 1f;
                        _moveDownCount.FillTime();
                    }

                    if (Input.GetKeyUp(KeyCode.LeftArrow) ||
                        Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        _waitCount.FillTime();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        Rotate();
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        Move(true);
                        _waitCount.FillTime();
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        Move(false);
                        _waitCount.FillTime();
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        MoveDown();
                        _moveDownCount.Value = 0.1f;
                        _moveDownCount.FillTime();
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        _waitCount.Tick(Time.deltaTime);
                        if (_waitCount.TimeOut)
                        {
                            _leftRightCount.Tick(Time.deltaTime);
                            if (_leftRightCount.TimeOut)
                            {
                                Move(true);
                                _leftRightCount.FillTime();
                            }
                        }
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        _waitCount.Tick(Time.deltaTime);
                        if (_waitCount.TimeOut)
                        {
                            _leftRightCount.Tick(Time.deltaTime);
                            if (_leftRightCount.TimeOut)
                            {
                                Move(false);
                                _leftRightCount.FillTime();
                            }
                        }
                    }

                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        _moveDownCount.Value = 1f;
                        _moveDownCount.FillTime();
                    }

                    if (Input.GetKeyUp(KeyCode.A) ||
                        Input.GetKeyUp(KeyCode.D))
                    {
                        _waitCount.FillTime();
                    }
                }


                // Move Down Detection
                _moveDownCount.Tick(Time.deltaTime);
                if (_moveDownCount.TimeOut)
                {
                    MoveDown();
                    _moveDownCount.FillTime();
                }
            }
        }

        private int RenderBrick()
        {
            // detect collision
            for (int row = 0; row < _brick.GetLength(0); row++)
            {
                for (int col = 0; col < _brick.GetLength(1); col++)
                {
                    if (_brick[row, col] == 1)
                    {
                        try
                        {
                            Vector3 pos = TetrisBehavior._boardPos[_curRow + row, _curCol + col];
                        }
                        catch (Exception e)
                        {
                            return 1;
                        }
                    }
                }
            }

            int count = 0;
            for (int row = 0; row < _brick.GetLength(0); row++)
            {
                for (int col = 0; col < _brick.GetLength(1); col++)
                {
                    if (_brick[row, col] == 1)
                    {
                        Vector3 pos = TetrisBehavior._boardPos[_curRow + row, _curCol + col];
                        _gridList[count].transform.position = pos;
                        count++;
                    }
                }
            }

            return 0;
        }


        /// <summary>
        /// Check whether the brick collide with the board
        /// </summary>
        /// <param name="curRow"></param>
        /// <param name="curCol"></param>
        /// <param name="brick"></param>
        /// <returns></returns>
        private bool IsCollide(int curRow, int curCol, int[,] brick = null)
        {
            if (brick == null)
            {
                brick = _brick;
            }

            int w = brick.GetLength(1);
            int h = brick.GetLength(0);
            for (int row = 0; row < h; row++)
            {
                for (int col = 0; col < w; col++)
                {
                    // index overflow -> collide!
                    try
                    {
                        if (brick[row, col] == 1)
                        {
                            if (row + curRow >= TetrisBehavior.Boardwidth ||
                                col + curCol >= TetrisBehavior.Boardheight ||
                                TetrisBehavior.Instance.board[row + curRow, col + curCol] == 1 ||
                                row + curRow < 0 || col + curCol < 0)
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Rotate Clockwise
        /// </summary>
        private void Rotate()
        {
            int w = _brick.GetLength(1);
            int h = _brick.GetLength(0);
            int[,] rotateBrick = new int[w, h];
            int[,] originBrick = new int[h, w];
            for (int row = 0; row < h; row++)
            {
                for (int col = 0; col < w; col++)
                {
                    rotateBrick[col, h - 1 - row] = _brick[row, col];
                    originBrick[row, col] = _brick[row, col];
                }
            }

            _brick = rotateBrick;
            if (IsCollide(_curRow, _curCol))
            {
                _brick = originBrick;
                return;
            }

            while (true)
            {
                int situation = RenderBrick();
                if (situation == 0)
                {
                    break;
                }

                if (situation == 1)
                {
                    _curCol -= 1;
                }
                else if (situation == 2)
                {
                    _curCol += 1;
                }
            }

            AudioManager.Instance.PlayRotateAudio();
        }

        /// <summary>
        /// Move Horizontally
        /// </summary>
        /// <param name="isLeft"></param>
        private void Move(bool isLeft)
        {
            int col = isLeft ? _curCol - 1 : _curCol + 1;
            if (!IsCollide(_curRow, col))
            {
                _curCol = col;
                RenderBrick();
                AudioManager.Instance.PlayMoveAudio();
            }
        }

        /// <summary>
        /// Move Down
        /// </summary>
        private void MoveDown()
        {
            int row = _curRow += 1;
            if (!IsCollide(row, _curCol))
            {
                _curRow = row;
                RenderBrick();
                AudioManager.Instance.PlayMoveAudio();
            }
            else
            {
                bFinish = true;
                for (int i = 0; i < _brick.GetLength(0); i++)
                {
                    for (int j = 0; j < _brick.GetLength(1); j++)
                    {
                        if (_brick[i, j] == 1)
                        {
                            TetrisBehavior.Instance.board[_curRow + i - 1, _curCol + j] = 1;
                            CameraBehaviour.Instance.OnShakeCamera(0.1f, 0f, 0.05f);
                        }
                    }
                }

                AudioManager.Instance.PlayThrowDownAudio();
            }
        }
    }
}