using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityGame : Game
{
    [SerializeField] private GameObject[] defaultPieces;
    [SerializeField] private PatternObject[] patterns;
    [SerializeField] private GameObject startPiece;
    [SerializeField] private Piece[ , ] board = new Piece[5, 5];
    [SerializeField] private bool[ , ] boardElectricity = new bool[5, 5];

    Vector3 startPiecePos;
    Vector3 startPiecePosGlobal;

    private void Start()
    {
        startPiecePos = startPiece.transform.localPosition;
        startPiecePosGlobal = startPiece.transform.position;
        Destroy(startPiece);
        
    }

    public override void Update()
    {
        if(wasBroken != isBroken)
        {
            if(board[0, 0] != null)
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Destroy(board[i, j].gameObject);
                }
            }

            board = new Piece[5, 5];
            boardElectricity = new bool[5, 5];

            NewGame();
            DoACycle();
            SetWasBroken(isBroken);
        }

        base.Update();
    }

    private void NewGame()
    {
        PatternObject pattern = patterns[Random.Range(0, patterns.Length)];
        int pieceCount = 0;

        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (pattern.boardPattern[pieceCount] != null)
                {
                    board[i, j] = Instantiate(pattern.boardPattern[pieceCount], startPiecePosGlobal, Quaternion.identity, gameObject.transform).GetComponent<Piece>();
                } else
                {
                    board[i, j] = Instantiate(defaultPieces[Random.Range(0, defaultPieces.Length)], startPiecePosGlobal, Quaternion.identity, gameObject.transform).GetComponent<Piece>();
                }
                board[i, j].transform.rotation = transform.rotation;
                board[i, j].transform.localPosition = new Vector3(startPiecePos.x, startPiecePos.y + (i * 0.1f), startPiecePos.z + (j * -0.1f));
                pieceCount++;
            }
        }
    }

    public void DoACycle()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                boardElectricity[i, j] = false;
                board[i, j].SetWire(false);
            }
        }

        if (board[0, 0].left)
        {
            boardElectricity[0, 0] = true;
        }
        else
        {
            boardElectricity[0, 0] = false;
        }

        board[0, 0].SetWire(boardElectricity[0, 0]);

        bool isChecked = false;
        int lastDir = 0;

        int x = 0;
        int y = 0;

        if(boardElectricity[0, 0])
        while (!isChecked)
        {
            bool turnedOn = false;
            if (y < 4)
            {
                if (board[y, x].up && board[y + 1, x].down && !boardElectricity[y + 1, x] && lastDir != 3)
                {
                    boardElectricity[y + 1, x] = true;
                    board[y + 1, x].SetWire(true);
                    lastDir = 1;
                    turnedOn = true;
                    y++;
                }
            }

            if (x < 4)
            {
                if (board[y, x].right && board[y, x + 1].left && !boardElectricity[y, x + 1] && lastDir != 4)
                {
                    boardElectricity[y, x + 1] = true;
                    board[y, x + 1].SetWire(true);
                    lastDir = 2;
                    turnedOn = true;
                    x++;
                }
            }

            if (x > 0)
            {
                if (board[y, x].left && board[y, x - 1].right && !boardElectricity[y, x - 1] && lastDir != 2)
                {
                    boardElectricity[y, x - 1] = true;
                    board[y, x - 1].SetWire(true);
                    lastDir = 4;
                    turnedOn = true;
                    x--;
                }
            }

            if (y > 0)
            {
                if (board[y, x].down && board[y - 1, x].up && !boardElectricity[y - 1, x] && lastDir != 1)
                {
                    boardElectricity[y - 1, x] = true;
                    board[y - 1, x].SetWire(true);
                    lastDir = 3;
                    turnedOn = true;
                    y--;
                }
            }

            if (y == 4 && x == 4)
            {
                isChecked = true;
                SetBroken(false);
                SetWasBroken(false);
            }
            else
            {
                if (!turnedOn)
                {
                    isChecked = true;
                }
            }
        }
    }
}
