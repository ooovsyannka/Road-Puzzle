using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : Window
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private RestartButton _restartButton;
    [SerializeField] private HomeButton _homeButton;

    private bool _isPause;

    private void OnEnable()
    {
        Close();
        _pauseButton.onClick.AddListener(Switch);
        _restartButton.Button.onClick.AddListener(Close);
        _homeButton.Button.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(Switch);
        _restartButton.Button.onClick.RemoveListener(Close);
        _homeButton.Button.onClick.RemoveListener(Close);
    }

    private void Switch()
    {
        if (_isPause)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public override void Open()
    {
        _restartButton.Open();
        _homeButton.Open();;
        Time.timeScale = 0;
        _isPause = true;

        base.Open();
    }

    public override void Close()
    {
        _restartButton.Close();
        _homeButton.Close();;
        Time.timeScale = 1;
        _isPause = false;

        base.Close();
    }
}
