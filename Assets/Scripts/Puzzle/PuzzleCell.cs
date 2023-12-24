using UnityEngine;

public class PuzzleCell : MonoBehaviour
{
    public int _cellPositionX;
    public int _cellPositionY;
    private (int, int) _cellPosition = default;

    private PuzzleTable _puzzleTable = default;

    // Start is called before the first frame update
    void Start()
    {
        _cellPosition = (_cellPositionX, _cellPositionY);

        _puzzleTable = GetComponentInParent<PuzzleTable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ピースが自身にカーソルを合わせていたら呼ばれる
    /// </summary>
    /// <param name="pieceInfo"></param>
    public void OnSelected(PieceInfo pieceInfo)
    {
        // 自分を光らせる処理

        _puzzleTable.CheckToSetable(pieceInfo, _cellPosition);
    }

    /// <summary>
    /// ピースがセル上で離されたときに呼ばれる
    /// </summary>
    public bool OnSelectExit(PieceInfo pieceInfo)
    {
        if (_puzzleTable.CanSet)
        {
            _puzzleTable.SetPiece(pieceInfo, _cellPosition);
            return true;
        }

        return false;
    }

    /// <summary>
    /// はめているピースが外されたとき
    /// </summary>
    public void OnRemovePiece(PieceInfo pieceInfo)
    {
        _puzzleTable.RemovePiece(pieceInfo, _cellPosition);
    }
}