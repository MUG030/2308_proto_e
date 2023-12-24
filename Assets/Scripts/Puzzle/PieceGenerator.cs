using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
/// <summary>
/// ピースを生成するためのクラス
/// </summary>
public class PieceGenerator : MonoBehaviour
{
    [Tooltip("生成する場所と数")]
    [SerializeField] private Transform[] _generationPos;
    [SerializeField] private GameObject[] _piecePrefab;
    [Tooltip("生成する際の親")]
    [SerializeField] private Transform _parent;

    void Start()
    {
        PieceGeneration();
    }

    /// <summary>
    /// ピースの生成
    /// </summary>
    private void PieceGeneration()
    {
        for (int i = 0; i < _generationPos.Length; i++)
        {
            var num = Random.Range(0, _piecePrefab.Length);
            var obj = Instantiate(_piecePrefab[num], _generationPos[i].position, _generationPos[i].rotation);
            //親の設定
            obj.transform.parent = _parent;

            //StatusのTypeをランダムに設定
            if (obj.TryGetComponent(out PuzzlePieces pieces))
            {
                var value = Random.Range(0, Enum.GetValues(typeof(PieceStatusType)).Length);
                pieces.SetPieceStatusType((PieceStatusType)value);
            }
        }
    }
}
