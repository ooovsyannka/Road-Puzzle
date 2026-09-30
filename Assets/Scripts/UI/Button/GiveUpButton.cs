using System;

public class GiveUpButton : ActionButton
{
    private const string GiveUp =  nameof(GiveUp)
        ;
    public  event Action<string> OnGiveUp;

    protected override void OnButtonAction()
    {
        OnGiveUp?.Invoke(GiveUp);
    }
}
