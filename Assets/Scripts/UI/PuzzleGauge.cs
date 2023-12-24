using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGauge : MonoBehaviour
{
    [Header("TimeのRectTransform")]
    [SerializeField] private RectTransform _rectCurrent;
    [Tooltip("バーの最大値")]
    [SerializeField] private float _maxNum;
    [Tooltip("バー最長の長さ")]
    private float _maxWidth;
    [Tooltip("現在の値")]
    private float _currentNum;

    private void Awake()
    {
        _maxWidth = _rectCurrent.sizeDelta.x;
        SetValue(0);
    }

    /// <summary>
    /// 値によってバーの長さを変える
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        _currentNum = value;    
        //バーの長さを更新
        _rectCurrent.SetWidth(GetWidth(value));
    }

    /// <summary>
    /// 現在の値に引数で与えられた値を足し算する
    /// </summary>
    /// <param name="value"></param>
    public void AddValue(float value) 
    {
        _currentNum += value;
        _rectCurrent.SetWidth(GetWidth(_currentNum));
    }

    private float GetWidth(float value)
    {
        float width = Mathf.InverseLerp(0, _maxNum, value);
        return Mathf.Lerp(0, _maxWidth, width);
    }
}

public static class UIExtensions
{
    /// <summary>
    /// 現在の値をRectにセットする
    /// </summary>
    /// <param name="width"></param>
    public static void SetWidth(this RectTransform rect, float width)
    {
        Vector2 s = rect.sizeDelta;
        s.x = width;
        rect.sizeDelta = s;
    }
}