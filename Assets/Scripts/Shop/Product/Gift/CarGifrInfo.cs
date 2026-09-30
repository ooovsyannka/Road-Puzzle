using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarGifrInfo : CarInfo
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _procentage;

    public void UpdateSlider(float completedProcent)
    {
        completedProcent = Mathf.Clamp01(completedProcent);
        _slider.value = completedProcent;
        _procentage.text = $"{Mathf.RoundToInt(completedProcent * 100f)}%";
    }
}
