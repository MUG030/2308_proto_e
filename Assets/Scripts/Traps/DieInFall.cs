using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieInFall : MonoBehaviour
{
	[SerializeField] GameOverActive gameOverActive;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Debug.Log("Dead!!!");
			StartCoroutine(GameOverCorutine(0.8f));
		}
	}
	private IEnumerator GameOverCorutine(float delay)
	{
		// 指定された秒数だけ待機
		yield return new WaitForSeconds(delay);
		gameOverActive.PlayerDeath(); // ここでプレイヤー死亡メソッドを呼び出す
	}
}

