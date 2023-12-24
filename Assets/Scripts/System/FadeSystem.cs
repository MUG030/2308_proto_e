using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeSystem : MonoBehaviour
{
    [SerializeField, Tooltip("Fadeのパネル")]
    private Image _image;

    [SceneName]
    [SerializeField] private string _nextScene;
    [SerializeField] private bool _isStart = false;

    void Start()
    {
        if (_isStart)
        {
            FadeIn();
        }
    }

    public void GoToTitleScene() 
    {
        _nextScene = "TitleScene";
        FadeOut();
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    public void FadeOut()
    {
        Debug.Log("呼ばれた");
        _image.enabled = true;
        _image.DOFade(1, 0.8f)
            .SetDelay(0.5f)
            //fadeoutが終わったら呼ばれる
            .OnComplete(() => SceneManager.LoadScene(_nextScene));
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    public void FadeIn()
    {
        _image.enabled = true;
        _image.DOFade(0, 1.5f)
        .SetDelay(0.5f)
        .OnComplete(() => _image.enabled = false);
    }
}
