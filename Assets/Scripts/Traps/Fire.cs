using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IDamageable
{
	private Vector2 MovedPosition;
	[SerializeField] float speed;

	public enum Fire_Type
    {
		Liner,
		Return,
    }
	[SerializeField] Fire_Type fire_Type;

	private Vector2 StartPos;
	[SerializeField] float MoveRange;
	Vector2 NowPosition;
	float NowSpeed;
	//  [SerializeField] SpriteRenderer _spriteRenderer;
	bool MoveFlag = false;
	GameObject player;

	[SerializeField] AudioSource audioSource;

    void Start()
	{
		MovedPosition = transform.position;
		player = GameObject.Find("Hero");

		StartPos = transform.position;
		NowPosition = StartPos;
		NowSpeed = speed;
	}

	void Update()
	{
		
		/*if (_spriteRenderer.isVisible)
		{
			MoveFlag = true;
		}
		*/
		if(this.transform.position.x - player.transform.position.x < 13 && player.transform.position.x > 1.6f)
        {
			MoveFlag = true;
        }

		if (!MoveFlag) return;

		if(fire_Type == Fire_Type.Liner)
        {
		MovedPosition.x -= speed * Time.deltaTime;
		this.transform.position = MovedPosition;
        }
		else if(fire_Type == Fire_Type.Return)
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
			this.transform.position = NowPosition;
		}
	}
	public int AddDamage()
	{
		audioSource.Play();
		return 1;
	}
}