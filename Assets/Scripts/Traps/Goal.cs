using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
	[SerializeField] private Timer timer;
	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("OnTriggerEntergaが呼ばれた!!!!");
		if (col.gameObject.tag == "Player")
		{
			Debug.Log("GOAL!!!!");
			this.gameObject.SetActive(false);
			timer.GameClear();
			SceneManager.LoadScene("ResultScene");
		}
		//TODO: ゴール画面への遷移
	}
}
