using TMPro;
using UnityEngine;

public class LevelWinHandler : MonoBehaviour
{
    private const int CoinsWhenAllGiftCarsAreUnlocked = 500;

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
        _winGameScreen.Open();
        _currentLevelData.CompleteLevel();

        int bonusCoinCount = 0;
        CarGift rewardCar = _carProductSaver.GetCurrentRewardCar();

        if (rewardCar != null)
        {
            _carProductSaver.AddProgressToCurrentRewardCar(procentageUnblockingCar, out float displayedProgress);
            _procentageUnblockingCarRender.UpdateSlider(displayedProgress);
        }
        else
        {
            // Once every gift car is unlocked, completed progress converts to coins.
            _carProductSaver.SaveProcentageUnblockingCar(procentageUnblockingCar);
            float coinProgress = _carProductSaver.GetProcentageUnblockingCar();

            while (coinProgress >= 1f)
            {
                bonusCoinCount += CoinsWhenAllGiftCarsAreUnlocked;
                _carProductSaver.SaveProcentageUnblockingCar(-1f);
                coinProgress -= 1f;
            }

            _procentageUnblockingCarRender.UpdateSlider(coinProgress);
        }

        int totalCoinCount = winCoinCount + bonusCoinCount;
        _wallet.AddCoin(totalCoinCount);
        _earnedCoinText.text = totalCoinCount.ToString();
    }
}
