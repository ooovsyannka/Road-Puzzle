using UnityEngine;

public class CloseWindowButton : ActionButton
{
    [SerializeField] private Window _window;

    protected override void OnButtonAction()
    {
        _window.Close();
    }
}