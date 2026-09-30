using UnityEngine;
using UnityEngine.UI;

public class HintPurchaseHandler : MonoBehaviour
{
    [SerializeField] private Hint _hint;
    [SerializeField] private Button _hintButton;
     
    private Wallet _wallet;
    
    private void OnEnable()
    {
        _hintButton.onClick.AddListener(AttemptShowHint);
    }

    private void OnDisable()
    {
        _hintButton.onClick.RemoveListener(AttemptShowHint);
    }

    public void Initialize(Wallet wallet, LevelData levelData)
    {
        _wallet = wallet;
        _hint.SetCost(levelData.HintSelection.Cost);
        _hint.SetSprite(levelData.HintSelection.HintImage);
    }
    
    public void DisableHintButton()
    {
        _hintButton.gameObject.SetActive(false);
    }
    
    private void AttemptShowHint()
    {
        if (_hint.IsBought == false)
        {
            if (_wallet.TryRemoveCoin(_hint.Cost))
            {
                _hint.Open();
                _hint.Buy();
            }
            else
            {
                print("Предлодить просмотр рекламы за подсказку ");
            }
        }
        else
        {
            _hint.Open();
        }
    }

}