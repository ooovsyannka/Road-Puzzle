using System;
using UnityEngine;

public class RoadRotaionBonusView : MonoBehaviour
{
    [SerializeField] private RoadRotationBonus _roadRotationBonus;
    [SerializeField] private RoadRotationActivationButton _activationButton;
    [SerializeField] private RoadRotationDeactivatingButton _deactivationButton;
    [SerializeField] private RoadRotationBonusRender _roadRotationBonusRender;
    [SerializeField] private WalletSaver _walletSaver;

    private void Awake()
    {
        UpdateWindowInfo();
    }

    private void OnEnable()
    {
        _roadRotationBonus.SelectionStateChanged += UpdateButtons;
    }

    private void OnDisable()
    {
        _roadRotationBonus.SelectionStateChanged -= UpdateButtons;
    }

    private void UpdateButtons(bool isSelectingRoad)
    {
        if (isSelectingRoad)
        {
            _activationButton.Close();
            _deactivationButton.Open();
        }
        else
        {
            _deactivationButton.Close();
            _activationButton.Open();
            UpdateWindowInfo();
        }
    }

    private void UpdateWindowInfo()
    {
        switch (_roadRotationBonus.FreeAttempts)
        {
            case > 0:
                _roadRotationBonusRender.ShowFreeItemCountWindow(_roadRotationBonus.FreeAttempts);
                break;
            default:
            {
                if (_walletSaver.HasEnoughCoins(_roadRotationBonus.Cost))
                {
                    _roadRotationBonusRender.ShowCostWindow();
                }
                else
                {
                    _roadRotationBonusRender.ShowOfferAdds();
                }

                break;
            }
        }
    }
}