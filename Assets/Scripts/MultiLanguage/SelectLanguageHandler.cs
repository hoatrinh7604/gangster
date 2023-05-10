using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLanguageHandler : MonoBehaviour {
    public Dropdown dropDownListMultiLanguage;

    private void Awake()
    {
        dropDownListMultiLanguage.value = LanguageHandler.currentLanguageIndex;
    }

    public void SelectLanguage()
    {
        LanguageHandler.Instance.SetLanguageIndex(dropDownListMultiLanguage.captionText.text);
    }
}
