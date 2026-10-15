using UnityEngine;
using UnityEngine.UI;
using YG;

public class CoinShop : Window
{
    [SerializeField] private Button _rewardCoinForAdButton;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        Close();
        _rewardCoinForAdButton.onClick.AddListener(Get100CoinsForAd);
    }

    private void OnDisable()
    {
        _rewardCoinForAdButton.onClick.RemoveListener(Get100CoinsForAd);
    }

    private void Get100CoinsForAd()
    {
        YG2.RewardedAdvShow("coins_100", () => { _wallet.AddCoins(100); });
    }
}