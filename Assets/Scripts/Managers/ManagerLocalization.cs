using UnityEngine;
using YG;
using TMPro;

public class ManagerLocalization : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    public static ManagerLocalization Instance { get; private set; }
    public LanguageType Language { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

            switch (YG2.envir.language)
            {
                case "en":
                    Language = LanguageType.English;
                    break;
                case "ru":
                    Language = LanguageType.Russian;
                    break;
                default:
                    Language = LanguageType.English;
                    break;
            }
        YG2.saves.language = Language;
        dropdown.value = (int)Language;

        UpdateAllTexts();
        dropdown.onValueChanged.AddListener(ChangeLocalization);
    }

    void UpdateAllTexts()
    {
        LocalizedText[] allTexts = FindObjectsByType<LocalizedText>(FindObjectsInactive.Include, FindObjectsSortMode.None); // true = включая неактивные
        foreach (var text in allTexts)
        {
            text.UpdateText();
        }
    }

    public void ChangeLocalization(int index)
    {
        YG2.saves.language = (LanguageType)index;
        Language = (LanguageType)index;
        UpdateAllTexts();
    }
}
