using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScore : MonoBehaviour, IScore
{
    [SerializeField] private int score = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int AddScore()
    {
        Destroy(gameObject);
        return score;
    }
}
