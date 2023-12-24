using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピースの情報を渡すためのクラス
/// </summary>
public class PieceInfo
{
    public int[,,] PieceArray => _pieceArray;
    public PieceStatusType PieceStatus => _pieceStatus;
    public PieceType PieceType => _pieceType;
    public int InscreasedNum => _increasedNum;
    public int RotateNum => _rotateNum;

    int[,,] _pieceArray;
    PieceStatusType _pieceStatus;
    PieceType _pieceType;
    int _increasedNum;
    int _rotateNum;

    public PieceInfo(int[,,] pieceArray, PieceStatusType pieceStatus, int increasedNum, PieceType pieceType)
    {
        _pieceArray = pieceArray;
        _pieceStatus = pieceStatus;
        _increasedNum = increasedNum;
        _pieceType = pieceType;
    }

    public void SetRotateNum(int rotateNum) 
    {
        _rotateNum = rotateNum;
    }
}
