using System;
using System.Collections.Generic;
using Tetris;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetrisBehavior : MonoBehaviour
{
    #region [Bricks]

    public static int[,] b1 =
    {
        { 1, 0 },
        { 1, 0 },
        { 1, 1 }
    };

    public static int[,] b2 =
    {
        { 0, 1 },
        { 0, 1 },
        { 1, 1 }
    };

    public static int[,] b3 =
    {
        { 0, 1 },
        { 1, 1 },
        { 1, 0 }
    };

    public static int[,] b4 =
    {
        { 1, 0 },
        { 1, 1 },
        { 0, 1 }
    };

    public static int[,] b5 =
    {
        { 1, 1 },
        { 1, 1 }
    };

    public static int[,] b6 =
    {
        { 1, 1, 1, 1 }
    };

    public static int[,] b7 =
    {
        { 0, 1 },
        { 1, 1 },
        { 0, 1 }
    };

    #endregion

    public int[,] board = new int[20, 10];
    public static Vector2[,] _boardPos = new Vector2[20, 10];
    public const int Boardwidth = 20;
    public const int Boardheight = 10;
    public List<GameObject> brickPrefabs;


    private int _nextBrickNum;
    public GameObject curBrick;
    public static TetrisBehavior Instance;

    public UIGamePage uiGamePage;
    private bool _bUpdate;

    private void Awake()
    {
        Instance = this;
        InitialBoard();
    }


    // Start is called before the first frame update
    void Start()
    {
        uiGamePage.Initial();
        _nextBrickNum = int.Parse(Random.Range(0, 7).ToString());
    }

    private void CreateBrick()
    {
        GameObject brick = Instantiate(brickPrefabs[_nextBrickNum], gameObject.transform, true);
        curBrick = brick;
        CreateNextBrick();
    }

    public void CreateNextBrick()
    {
        _nextBrickNum = int.Parse(Random.Range(0, 7).ToString());
        uiGamePage.OnDisplayNextBrick(brickPrefabs[_nextBrickNum].name.Replace("Brick", ""));
    }

    private static void InitialBoard()
    {
        float x = -2.25f;
        float y = 4.75f;
        for (int i = 0; i < Boardwidth; i++)
        {
            float posY = y - i * 0.5f;
            for (int j = 0; j < Boardheight; j++)
            {
                float posX = x + j * 0.5f;
                _boardPos[i, j] = new Vector2(posX, posY);
            }
        }
    }

    public void OnStartUpdate()
    {
        UIGamePage.Instance.OnStartGame();
        _bUpdate = true;
        CreateBrick();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bUpdate)
        {
            if (curBrick.transform.GetComponent<BrickBehavior>().bFinish)
            {
                CreateBrick();
            }
        }
    }
}