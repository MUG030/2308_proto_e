using UnityEngine;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour
{
    [SerializeField]
    private GameState _gameState;
    [SerializeField]
    private PuzzleTable _puzzleTable;
    [SerializeField] 
    private float _timeLimit;
    [SerializeField]
    private Text _timerText;
	[SerializeField] private AudioManager audioManager;
	private void Update()
    {
        if (_timeLimit > 0)
        {
            _timeLimit -= Time.deltaTime;
            UpdateTimer();
        }
        else 
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        Debug.Log("�p�Y���̏I�����I������܂����B");
        _puzzleTable.FinishPuzzlePhase();
        _gameState.ChangeState(GameStateType.MainGameState);
		audioManager.MainBGM();
	}

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(_timeLimit / 60);
        int seconds = Mathf.FloorToInt(_timeLimit % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
