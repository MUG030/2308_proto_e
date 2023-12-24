using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverActive : MonoBehaviour
{
	[SerializeField] GameObject gameOverCanvas;
	[SerializeField] AudioSource bgmAudioSource;

	private void Start()
	{
		gameOverCanvas.SetActive(false);
	}
	public void PlayerDeath()
	{
		StartCoroutine(FadeOutBGM(2.0f));
		gameOverCanvas.SetActive(true);
		// gameOverCanvas.SetActive(true); // プレイヤーが死んだときにアクティブにする
	}
	IEnumerator FadeOutBGM(float fadeTime)
	{
		float startVolume = bgmAudioSource.volume;

		while (bgmAudioSource.volume > 0)
		{
			bgmAudioSource.volume -= startVolume * Time.deltaTime / fadeTime;

			yield return null;
		}

		bgmAudioSource.Stop();
		bgmAudioSource.volume = startVolume;
	}
}
