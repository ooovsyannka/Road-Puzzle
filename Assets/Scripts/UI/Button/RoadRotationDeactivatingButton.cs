using UnityEngine;

public class RoadRotationDeactivatingButton : ActionButton
{
    [SerializeField] private RoadRotationBonus _roadRotationBonus;
    
    protected override void OnButtonAction()
    {
        _roadRotationBonus.StopSelectingRoad();
    }
}