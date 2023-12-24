using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class GameState : MonoBehaviour
{
    [Tooltip("���Վ��_�ɂ����鎞��")]
    [SerializeField] private float _overheadViewNum;
    [SerializeField] private GameObject _puzzleObj;
    [SerializeField] private GameObject _cinemachineObj;
    [SerializeField] private NewPlayerMove _playerMove;
    [SerializeField] private Timer _timer;
	[SerializeField] private AudioManager audioManager;
    private GameStateType _gameStateType = GameStateType.OverheadViewState;
    private Vector2 _outDirection = Vector2.up;
    //private WaitForSeconds _puzzleOutWait = new WaitForSeconds(0.01f);

    private void Start()
    {
        ChangeState(GameStateType.OverheadViewState);
    }

    private void Update()
    {

    }

    /// <summary>
    /// �X�e�[�g��ύX����
    /// </summary>
    /// <param name="gameStateType"></param>
    public async void ChangeState(GameStateType gameStateType)
    {
        _gameStateType = gameStateType;
        await ChangeStateEvent();
    }

    /// <summary>
    /// �ύX���ꂽ�X�e�[�g�ɔ��������������s����
    /// </summary>
    /// <returns></returns>
    public async UniTask ChangeStateEvent()
    {
        if (_gameStateType == GameStateType.OverheadViewState)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_overheadViewNum));
            _cinemachineObj.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(_overheadViewNum));
            ChangeState(GameStateType.PuzzleState);
        }
        else if (_gameStateType == GameStateType.PuzzleState)
        {

            Debug.Log("�p�Y���ł�");
            _puzzleObj.SetActive(true);
			audioManager.PuzzleBGM();

		}
        else if (_gameStateType == GameStateType.MainGameState)
        {
            if (_puzzleObj) 
            {
                _puzzleObj.SetActive(false);
            }
            else 
            {
               var obj = GameObject.FindWithTag("GameController");
               obj.SetActive(false);
            }
            //Player�𓮂����n�߂�悤�Ȋ֐����Ă�
            if (_playerMove)
            {
                _playerMove.CanMove = true;
            }
            else 
            {
                _playerMove = GameObject.FindObjectOfType<NewPlayerMove>();
                _playerMove.CanMove = true;
            }
            
            _timer.Puzzle = true;
			// audioManager.MainBGM();
        }
    }

    /// <summary>
    /// �p�Y�����e�B���g�����鏈��
    /// </summary>
    /// <returns></returns>
    private void PuzzleOut()
    {
        RectTransform _currentPosition = (RectTransform)_puzzleObj.transform;
        float currentTime = 0f;

        while (((RectTransform)_puzzleObj.transform).position.y >= _currentPosition.position.y)
        {
            if (currentTime >= 0.01f)
            {
                ((RectTransform)_puzzleObj.transform).Translate(_outDirection);
                currentTime = 0f;
            }

            currentTime += Time.deltaTime;
        }
    }
}

public enum GameStateType
{
    OverheadViewState,
    PuzzleState,
    MainGameState,
}
