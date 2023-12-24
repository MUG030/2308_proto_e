using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private PlayerHP _playerHP;

    private int moveSpeed = 0;
    private int jumpForce = 0;
    private int _maxHpValue = 20;

    public int MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
    public int JumpForce
    {
        get
        {
            return jumpForce;
        }
    }

    public void AddMoveSpeed(int amount)
    {
        moveSpeed += amount;
    }
    public void AddJumpForce(int amount)
    {
        jumpForce += amount;
    }
    public void AddHP(int amount)
    {
        if (amount > _maxHpValue) amount = 20;
        _playerHP.SettingHp(amount);
    }
}
