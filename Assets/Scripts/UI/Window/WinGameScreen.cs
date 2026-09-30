using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class WinGameScreen : Window
{
    [SerializeField] private Button _actionButton;

    private void OnEnable()
    {
        Close();
        _actionButton.onClick.AddListener(LoadMenu);
    }

    private void LoadMenu()
    {
        MainMenu.Load();
    }
}