using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour
{
    [SerializeField] private CarContainersHolder _carContainersesHolder;
    [SerializeField] private WalletRender _walletRender;
    [SerializeField] private Button _buyCarButton;
    [SerializeField] private Button _selectCarButton;
    [SerializeField] private Button _closeCarInfoButton;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private InsufficientFundsDisplay _insufficientFundsDisplay;
    [SerializeField] private CarProductSaver _carProductSaver;
    [SerializeField] private Wallet _wallet;
    
    private Coroutine _moveContentCoroutine;
    private CarProduct _carProduct;
    private CarContainer _currentCarContainer;
    private float _duration = 0.5f;

    private void OnEnable()
    {
        _buyCarButton.onClick.AddListener(TrySellGoods);
        _closeCarInfoButton.onClick.AddListener(CloseCarInfoButton);
        _buyCarButton.gameObject.SetActive(false);
        _selectCarButton.gameObject.SetActive(false);
        _closeCarInfoButton.gameObject.SetActive(false);
        _carContainersesHolder.CarContainerSelected += ShowCarInfo;
    }

    private void OnDisable()
    {
        _buyCarButton.onClick.RemoveListener(TrySellGoods);
        _closeCarInfoButton.onClick.RemoveListener(CloseCarInfoButton);
        _carContainersesHolder.CarContainerSelected -= ShowCarInfo;
    }

    private void TrySellGoods()
    {
        if (_carProduct is not CarGoods carGoods
            || _carProductSaver.IsCarBought(carGoods))
            return;

        if (_wallet.TryRemoveCoin(carGoods.Price))
        {
            carGoods.Buy();
            _carProductSaver.SaveCarProduct(_carProduct);
            _carProductSaver.SaveSelectCarProduct(_carProduct);
            _currentCarContainer.CarInfo.UpdateLockImage(true);
            _currentCarContainer.CarInfo.UpdateSelectImage(true);
            _buyCarButton.gameObject.SetActive(false);
            _selectCarButton.gameObject.SetActive(true);
        }
        else
        {
            _insufficientFundsDisplay.Open();
        }
    }
    
    private void ShowCarInfo(CarContainer carContainer)
    {
        _carProduct = carContainer.CarProduct;
        _currentCarContainer = carContainer;
        _scrollRect.horizontal = false;

        if (_moveContentCoroutine != null)
            StopCoroutine(_moveContentCoroutine);

        _moveContentCoroutine = StartCoroutine(
            SmoothlySetCarGoodsPosition(carContainer));
    }
    
    private void CloseCarInfoButton()
    {
        _scrollRect.horizontal = true;
        _buyCarButton.gameObject.SetActive(false);
        _selectCarButton.gameObject.SetActive(false);
        _closeCarInfoButton.gameObject.SetActive(false);
        _currentCarContainer.CloseCarInfo();
    }

    private IEnumerator SmoothlySetCarGoodsPosition(CarContainer carContainer)
    {
        _closeCarInfoButton.gameObject.SetActive(true);

        Canvas.ForceUpdateCanvases();

        RectTransform viewport = _scrollRect.viewport != null
            ? _scrollRect.viewport
            : (RectTransform)_scrollRect.transform;

        Vector3 itemCenter = carContainer.RectTransform.TransformPoint(
            carContainer.RectTransform.rect.center);

        Vector3 viewportCenter = viewport.TransformPoint(viewport.rect.center);

        Vector3 localOffset = _contentRectTransform.parent.InverseTransformVector(
            viewportCenter - itemCenter);

        Vector2 startPosition = _contentRectTransform.anchoredPosition;
        Vector2 targetPosition = startPosition + new Vector2(localOffset.x, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            float time = Mathf.Clamp01(elapsedTime / _duration);
            _contentRectTransform.anchoredPosition =
                Vector2.Lerp(startPosition, targetPosition, time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _contentRectTransform.anchoredPosition = targetPosition;
        _moveContentCoroutine = null;

        CarProduct carProduct = carContainer.CarProduct;

        if (_carProductSaver.IsCarBought(carProduct))
            _selectCarButton.gameObject.SetActive(true);
        else
            _buyCarButton.gameObject.SetActive(true);
    }
}