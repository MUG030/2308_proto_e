using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour, IDamageable
{
	[SerializeField] AudioSource audioSource;
	void Start()
	{
		// Vector3 position = transform.position;
		// Debug.Log("Needleã®åº§æ¨™: " + position.ToString());
	}


	public int AddDamage()
	{
		audioSource.Play();
		return 1;
	}

}