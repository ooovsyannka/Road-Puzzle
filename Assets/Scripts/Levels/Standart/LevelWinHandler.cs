using TMPro;
using UnityEngine;

public class LevelWinHandler : MonoBehaviour
{
    [SerializeField] private LevelSaver _levelSaver;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _earnedCoinText;
    [SerializeField] private CarProductSaver _carProductSaver;
    [SerializeField] private ProcentageUnblockingCarRender _procentageUnblockingCarRender;
    [SerializeField] private WinGameScreen _winGameScreen;
  
    private LevelData _currentLevelData;

    public void Initialize(LevelData levelData)
    {
        _currentLevelData = levelData;
    }
    
    public void Win()
    {
        _levelSaver.SaveLevel(_currentLevelData);
        int winCoinCount = _currentLevelData.WinCoinCount;
        float procentageUnblockingCar = _currentLevelData.ProcentageUnblockingCar;
        print($"{procentageUnblockingCar} procentageUnblockingCar");
        _winGameScreen.Open();
        _currentLevelData.CompleteLevel();
        _wallet.AddCoin(winCoinCount);
        _earnedCoinText.text = winCoinCount.ToString();
        _carProductSaver.SaveProcentageUnblockingCar(procentageUnblockingCar);

        _procentageUnblockingCarRender.UpdateSlider(_carProductSaver.GetProcentageUnblockingCar());
    }
}