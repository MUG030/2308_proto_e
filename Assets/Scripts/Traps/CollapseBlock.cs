using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollapseBlock : MonoBehaviour,IReceiveable
{
    GameObject player;
    float count;
    [SerializeField] float LimitTIme;
    bool isFall;
    bool isTouch;
    bool isOnce;

    [SerializeField] SpriteRenderer spriteRenderer;
    Vector4 colors = new Vector4(1, 1, 1, 1 );
    void Start()
    {
        player = GameObject.Find("Hero");
    }

    void Update()
    {
        if (isTouch)
        {
            count += Time.deltaTime;
            spriteRenderer.color = colors;
            colors.y -= 0.01f;
            colors.z -= 0.01f;
            if(colors.y < 0)
            {
                colors.y = 0;
            }
            if (colors.z < 0)
            {
                colors.z = 0;
            }
            if (count > LimitTIme)
            {
                if (!isFall)
                {
                    isFall = true;
                }
            }
            if (isFall)
            {
                if (isOnce) return;
                isOnce = true;
                transform.DOLocalMoveY(-15, 5).SetRelative(true).SetEase(Ease.Linear).OnComplete(() => Destroy(this.gameObject));
            }
        }
        else
        {
            colors.y = 1;
            colors.z = 1;
            count = 0;
        }
    }

    public float Collide()
    {
        if (isTouch)
        {
            isTouch = false;
            colors.y = 1;
            colors.z = 1;
            count = 0;
        }
        else
        {
            if(player.transform.position.y > this.transform.position.y)
            {
            isTouch = true;
            }
        }
        return 1;
    }
}
