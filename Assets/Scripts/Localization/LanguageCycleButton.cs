using UnityEngine;
using Lean.Localization;
using UnityEngine.UI;

public class LanguageCycleButton : MonoBehaviour
{
    [SerializeField] private Button _languageButton; 
    [SerializeField] private LanguageCycleSaver _languageCycleSaver;
    
    private string[] languages =
    {
        "Russian",
        "English",
        "Turkish"
    };
    
    private int currentIndex ;

    private void Awake()
    {
        currentIndex = _languageCycleSaver.GetLanguageIndex();
    }

    private void OnEnable()
    {
        _languageButton.onClick.AddListener(SelectNextLanguage);
    }

    private void SelectNextLanguage()
    {
        if (languages == null || languages.Length == 0)
            return;

        currentIndex = (currentIndex + 1) % languages.Length;
        _languageCycleSaver.SaveLanguageIndex(currentIndex);
        LeanLocalization.SetCurrentLanguageAll(languages[currentIndex]);
    }
}