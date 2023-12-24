using UnityEngine;

public class PuzzleTable : MonoBehaviour
{
    [SerializeField, Tooltip("HP増加バー")]
    private PuzzleGauge _hpGauge;
    [SerializeField, Tooltip("ジャンプ増加バー")]
    private PuzzleGauge _jumpGauge;
    [SerializeField, Tooltip("スピード増加バー")]
    private PuzzleGauge _speedGauge;

    private enum TableSize
    {
        Three,
        five
    }
    [SerializeField, Tooltip("パズルテーブルの大きさ")]
    private TableSize _tableSize = default;

    [Tooltip("パズルの一辺の大きさ")]
    private int _puzzleSize = default;
    [Tooltip("_tableArrayの左上セルのポインタ")]
    private (int x, int y) _startCellPointer = default;

    [Tooltip("パズルテーブルを管理する配列")]
    private int[,] _tableArray = default;

    [Tooltip("ピースをセット可能")]
    private bool _canSet = false;

    private int _hp = default;
    private int _jump = default;
    private int _speed = default;

    /// <summary>
    /// ピースをセット可能
    /// </summary>
    public bool CanSet
    {
        get
        {
            return _canSet;
        }
    }


    private void Start()
    {
        //CheckToSetable(new PieceInfo(new int[3, 3] { { 1, 0, 0 }, { 1, 0, 0 }, { 0, 0, 0 } }, PieceStatusType.Hp, 1, PieceType.IPiece), (0, 0));

        // プレハブの種類によって、パズルの大きさを初期化
        switch (_tableSize)
        {
            case TableSize.Three:
                _puzzleSize = 3;
                _tableArray = new int[7, 7]
                {
                    {1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 0, 0, 0, 1, 1},
                    {1, 1, 0, 0, 0, 1, 1},
                    {1, 1, 0, 0, 0, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1}
                };

                _startCellPointer = (2, 2);
                break;

            case TableSize.five:
                _puzzleSize = 5;
                _tableArray = new int[13, 13]
                {
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                };

                _startCellPointer = (4, 4);
                break;
        }

        // ゲージの初期化
        _hpGauge.SetValue(0);
        _jumpGauge.SetValue(0);
        _speedGauge.SetValue(0);
    }


    /// <summary>
    /// ピースをセットできるかチェックする
    /// </summary>
    public void CheckToSetable(PieceInfo pieceInfo, (int x, int y) cellPosition)
    {
        (int x, int y) tableArrayPointer = (_startCellPointer.x + cellPosition.x, _startCellPointer.y + cellPosition.y);

        // 渡されたピースの3*3の配列を、左上から順番にチェックする
        for (int yy = 0; yy < 3; yy++)
        {
            for (int xx = 0; xx < 3; xx++)
            {
                // 空白のマス（形がない箇所）ならスキップして次の比較へ
                if (pieceInfo.PieceArray[pieceInfo.RotateNum, yy, xx] == 0) continue;

                // すでに他のピースがはまっているか、枠外だったら
                if (_tableArray[tableArrayPointer.y + yy, tableArrayPointer.x + xx] == 1)
                {
                    _canSet = false;
                    return;
                }
            }
        }

        _canSet = true;
    }

    /// <summary>
    /// ピースをはめる
    /// </summary>
    /// <param name="pieceInfo"></param>
    /// <param name="pointer"></param>
    public void SetPiece(PieceInfo pieceInfo, (int x, int y) cellPosition)
    {
        (int x, int y) tableArrayPointer = (_startCellPointer.x + cellPosition.x, _startCellPointer.y + cellPosition.y);

        for (int yy = 0; yy < 3; yy++)
        {
            for (int xx = 0; xx < 3; xx++)
            {
                if (pieceInfo.PieceArray[pieceInfo.RotateNum, yy, xx] == 0) continue;

                _tableArray[tableArrayPointer.y + yy, tableArrayPointer.x + xx] = 1;
            }
        }

        _canSet = false;

        switch (pieceInfo.PieceStatus)
        {
            case PieceStatusType.Hp:
                _hp += pieceInfo.InscreasedNum;
                _hpGauge.AddValue(pieceInfo.InscreasedNum);
                break;

            case PieceStatusType.Jump:
                _jump += pieceInfo.InscreasedNum;
                _jumpGauge.AddValue(pieceInfo.InscreasedNum);
                break;

            case PieceStatusType.Speed:
                _speed += pieceInfo.InscreasedNum;
                _speedGauge.AddValue(pieceInfo.InscreasedNum);
                break;
        }

#if UNITY_EDITOR
        PuzzleDebug();
#endif
    }

    /// <summary>
    /// ピースを外す
    /// </summary>
    /// <param name="pieceInfo"></param>
    /// <param name="cellPosition"></param>
    public void RemovePiece(PieceInfo pieceInfo, (int x, int y) cellPosition)
    {
        (int x, int y) tableArrayPointer = (_startCellPointer.x + cellPosition.x, _startCellPointer.y + cellPosition.y);

        for (int yy = 0; yy < 3; yy++)
        {
            for (int xx = 0; xx < 3; xx++)
            {
                if (pieceInfo.PieceArray[pieceInfo.RotateNum, yy, xx] == 0) continue;

                _tableArray[tableArrayPointer.y + yy, tableArrayPointer.x + xx] = 0;
            }
        }

        switch (pieceInfo.PieceStatus)
        {
            case PieceStatusType.Hp:
                _hpGauge.AddValue(-pieceInfo.InscreasedNum);
                break;

            case PieceStatusType.Jump:
                _jumpGauge.AddValue(-pieceInfo.InscreasedNum);
                break;

            case PieceStatusType.Speed:
                _speedGauge.AddValue(-pieceInfo.InscreasedNum);
                break;
        }

#if UNITY_EDITOR
        PuzzleDebug();
#endif
    }

    /// <summary>
    /// パズルフェーズを終了してステータスを送信
    /// </summary>
    public void FinishPuzzlePhase()
    {
        Debug.Log("ステータス送信！" + "\n" +
            "HP：" + _hp + "\n" +
            "Jump：" + _jump + "\n" +
            "Speed：" + _speed);

        NewPlayerMove move = GameObject.FindWithTag("Player").GetComponent<NewPlayerMove>();
        PlayerHP hp = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();
        move.JumpValue = _jump;
        move.SpeedValue = _speed;
        hp.HpValue = _hp;
    }

#if UNITY_EDITOR
    /// <summary>
    /// 現在のパズルの情報をコンソールに表示する
    /// </summary>
    private void PuzzleDebug()
    {
        string comma = ", ";
        int x = _startCellPointer.x;
        int y = _startCellPointer.y;

        var log = "\n";
        for (int yy = 0; yy < _puzzleSize; yy++)
        {
            for (int xx = 0; xx < _puzzleSize; xx++)
            {
                log += _tableArray[y + yy, x + xx] + comma;
            }

            log += "\n";
        }

        Debug.Log(log);
    }
#endif
}
