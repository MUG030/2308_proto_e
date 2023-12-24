using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDownFloor : MonoBehaviour,IReceiveable
{
    [SerializeField] AudioSource audios;
    public float Collide()
    {
        if(audios!= null) { 
            audios.Play();
        }
        return 0.6f;
    }


   
}


