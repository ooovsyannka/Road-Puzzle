using TMPro;
using UnityEngine;

public class RoadRotationBonusRender : MonoBehaviour
{
    private const int WindowMaxAlpha = 1;

    [SerializeField] private TextMeshProUGUI _freeItemCountText;
    [SerializeField] private CanvasGroup _freeItemCountWindow;
    [SerializeField] private CanvasGroup _costWindow;
    [SerializeField] private CanvasGroup _offerAdds;

    public void ShowFreeItemCountWindow(int count)
    {
        _freeItemCountText.text = count.ToString();
        _freeItemCountWindow.alpha = WindowMaxAlpha;

        _costWindow.alpha = 0;
        _offerAdds.alpha = 0;
    }

    public void ShowCostWindow()
    {
        _costWindow.alpha = WindowMaxAlpha;

        _offerAdds.alpha = 0;
        _freeItemCountWindow.alpha = 0;
    }

    public void ShowOfferAdds()
    {
        _offerAdds.alpha = WindowMaxAlpha;

        _freeItemCountWindow.alpha = 0;
        _costWindow.alpha = 0;
    }
}