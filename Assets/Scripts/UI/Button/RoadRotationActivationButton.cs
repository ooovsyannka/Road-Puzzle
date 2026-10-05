
using UnityEngine;

public class RoadRotationActivationButton : ActionButton
{
    [SerializeField] private RoadRotationBonus _roadRotationBonus;
    
    protected override void OnButtonAction()
    {
        _roadRotationBonus.StartSelectingRoad();
    }
}