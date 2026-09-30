using UnityEngine;
using UnityEngine.UI;

public class AddCoinButton : ActionButton
{
    [SerializeField] private CoinShop _coinShop;
    
    protected override void OnButtonAction()
    {
        _coinShop.Open();
    }
}