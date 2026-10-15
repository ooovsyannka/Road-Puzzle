using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProcentageUnblockingCarRender : MonoBehaviour
{
    [SerializeField] private Image _image;

    private float _duration = 1.25f;
    private Coroutine _fillCoroutine;

    public void UpdateImage(float value, Sprite carGiftImage)
    {
        _image.sprite = carGiftImage;
        FillTo(value);
    }

    private void FillTo(float targetValue)
    {
        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(FillCoroutine(targetValue));
    }

    private IEnumerator FillCoroutine(float targetValue)
    {
        float elapsed = 0f;
        float progress;
        float smothlyFillCount;
        _image.fillAmount = 0f;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            progress = Mathf.Clamp01(elapsed / _duration);
            smothlyFillCount = Mathf.Lerp(0f, targetValue, progress);
            _image.fillAmount = smothlyFillCount;

            yield return null;
        }

        _image.fillAmount = targetValue;
        _fillCoroutine = null;
    }
}