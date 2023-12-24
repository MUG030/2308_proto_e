using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class PuzzleText : MonoBehaviour
{
    [SerializeField] private float _moveY;
    [SerializeField] private Text _text;
    private Tween _tween;
    // Start is called before the first frame update
    async void Start()
    {
        transform.DOMove(new Vector3(transform.position.x, _moveY, 0), 2f)
              .SetEase(Ease.OutQuad).OnComplete(() => 
              {
                  _tween.Kill();
                  Destroy(this.gameObject);
              } );
        _tween = transform.DOMoveX(transform.position.x + 50, 0.5f)
                      .SetLoops(-1, LoopType.Yoyo);

        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValueColorText(Color color, int value) 
    {
   
    }
}
