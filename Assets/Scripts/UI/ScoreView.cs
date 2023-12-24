using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    void Start()
    {
        _scoreText.DOCounter(0, ScoreManager.score, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
