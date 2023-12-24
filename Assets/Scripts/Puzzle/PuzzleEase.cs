using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleEase : MonoBehaviour
{
    [SerializeField] private float _moveY;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(transform.position.x, _moveY, 0), 1f)
                 .SetEase(Ease.OutBounce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
