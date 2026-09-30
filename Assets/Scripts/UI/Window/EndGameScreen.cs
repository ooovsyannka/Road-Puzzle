using Lean.Localization;
using TMPro;
using UnityEngine;

public class EndGameScreen : Window
{
    [SerializeField] private RestartButton _restartButton;
    [SerializeField] private AddCoinButton _addCoinButton;
    [SerializeField] private TextMeshProUGUI _finishGameText;
    [SerializeField] private HomeButton _homeButton;

    private void OnEnable()
    {
        Close();
    }

    public void SetLevelMode(LevelMode levelMode, LevelData levelData = null)
    {
        _restartButton.SetLevelMode(levelMode, levelData);
    }

    public override void Close()
    {
        base.Close();
        _restartButton.Close();
        _homeButton.Close();
        _addCoinButton.Close();
    }

    public override void Open()
    {
        base.Open();
        _restartButton.Open();
        _homeButton.Open();
        _addCoinButton.Open();
    }

    public void ShowLoosInfo(string textFinishGame)
    {
        _finishGameText.text = LeanLocalization.GetTranslationText(textFinishGame);
    }
}
