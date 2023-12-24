using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioSource bgmAudioSource;
	//BGM
	[SerializeField] AudioClip timerClip;
	[SerializeField] AudioClip bgmClip;

	private void Start()
	{
		bgmAudioSource.volume = 0.3f;
		bgmAudioSource.loop = true; // BGMをループ再生する
	}
	public void PuzzleBGM()
	{
		bgmAudioSource.clip = timerClip;
		bgmAudioSource.Play();
	}
	public void MainBGM()
	{
		Debug.Log("MainBGMが呼ばれた");
		// bgmAudioSource.loop = false; // BGMをループ再生する
		bgmAudioSource.clip = bgmClip;
		bgmAudioSource.Play();
	}
	/*
	//SE
	[SerializeField] AudioSource seAudioSource;
	[SerializeField] AudioClip soundEffect1;
	[SerializeField] AudioClip soundEffect2;
	[SerializeField] AudioClip soundEffect3;
	public void PlaySoundEffect1()
	{
		seAudioSource.PlayOneShot(soundEffect1);
	}

	public void PlaySoundEffect2()
	{
		seAudioSource.PlayOneShot(soundEffect2);
	}

	public void PlaySoundEffect3()
	{
		seAudioSource.PlayOneShot(soundEffect3);
	}
	*/
}
