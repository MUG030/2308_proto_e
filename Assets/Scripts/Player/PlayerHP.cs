using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameOverActive _gameOverActive;

    private int _hpValue = default;
    private int _hp = 1;
    [SerializeField] GameObject _panel;

    [SerializeField] private GameObject lifeObj;

    public int HpValue
    {
        set
        {
            _hpValue = value;
            _hpValue /= 5;

            if (_hpValue > 4) _hpValue = 4;
            _hp += _hpValue;

            SetLifeGauge(_hp);
        }
    }

    void Start()
    {
       // SetLifeGauge(_hp);
    }

    public void SettingHp(int hp)
    {
       // _hp += hp / 5;

       // SetLifeGauge(_hp);
    }

    public void SetLifeGauge(int life)
    {
        Debug.Log("Life:"+life);
        for (int i = 0; i < life; i++)
        {
            //Instantiate<GameObject>(lifeObj, _panel.transform);
            GameObject go = Instantiate(lifeObj);
            go.GetComponent<RectTransform>().transform.parent = _panel.transform.parent;
            go.transform.SetParent(_panel.transform, false);
            Debug.Log("–°‚¢–°‚¢");
        }
    }
    public void SetLifeGauge2(int damage)
    {
        if (_hp - damage > 0)
        {
            _hp -= damage;
            for (int i = 0; i < damage; i++)
            {
                Destroy(_panel.transform.GetChild(i).gameObject);
            }
        } else
        {
            for (int i = 0; i < _hp; i++)
            {
                Destroy(_panel.transform.GetChild(i).gameObject);
            }
            _hp = 0;

            _gameOverActive.PlayerDeath();
        }
    }
}
