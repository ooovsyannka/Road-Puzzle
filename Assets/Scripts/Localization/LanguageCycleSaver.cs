using UnityEngine;

public class LanguageCycleSaver : MonoBehaviour
{
    private const string  LanguageIndex = nameof(LanguageIndex);

    public void SaveLanguageIndex(int languageIndex)
    {
        PlayerPrefs.SetInt(LanguageIndex, languageIndex);
    }

    public int GetLanguageIndex()
    {
        return PlayerPrefs.GetInt(LanguageIndex);
    }
}