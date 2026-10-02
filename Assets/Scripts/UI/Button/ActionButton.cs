using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : Window
{
    [SerializeField] private Button _button;

    public Button Button =>  _button;

    protected void OnEnable()
    {
        _button.onClick.AddListener(OnButtonAction);
    }

    protected void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonAction);
    }

    protected abstract void OnButtonAction();
}
