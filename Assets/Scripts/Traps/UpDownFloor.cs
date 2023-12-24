using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFloor : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 StartPos;
    [SerializeField] float MoveRange;
    public enum Floor_Type
    {
        UP_DOWN,
        LEFT_RIGHT,
    }
    [SerializeField] Floor_Type floor_Type;

    Vector2 NowPosition;
    float NowSpeed;

    private void Start()
    {
		StartPos = transform.position;
        NowPosition = StartPos;
        NowSpeed = speed;
    }
    void Update()
    {
        if (floor_Type == Floor_Type.UP_DOWN)
        {
            NowPosition.y += NowSpeed * Time.deltaTime;
            if (NowPosition.y > StartPos.y + MoveRange)
            {
                NowPosition.y = StartPos.y + MoveRange;
                NowSpeed = -speed;
            }
            else if (NowPosition.y < StartPos.y - MoveRange)
            {
                NowPosition.y = StartPos.y - MoveRange;
                NowSpeed = speed;
            }
        }
        else if(floor_Type == Floor_Type.LEFT_RIGHT)
        {
            NowPosition.x += NowSpeed * Time.deltaTime;
            if (NowPosition.x > StartPos.x + MoveRange)
            {
                NowPosition.x = StartPos.x + MoveRange;
                NowSpeed = -speed;
            }
            else if (NowPosition.x < StartPos.x - MoveRange)
            {
                NowPosition.x = StartPos.x - MoveRange;
                NowSpeed = speed;
            }
        }
        this.transform.position = NowPosition;
    }
}
