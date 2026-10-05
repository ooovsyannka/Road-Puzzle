using UnityEngine;

public class SettingButton : ActionButton
{
    [SerializeField] private SettingWindow _settingWindow;
    protected override void OnButtonAction()
    {
        _settingWindow.Open();
    }
}