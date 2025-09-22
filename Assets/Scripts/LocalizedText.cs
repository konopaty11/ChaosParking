using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] string translationKey;

    Dictionary<string, string> enTranslation = new()
    {
        { "logo", "Chaos Parking" },
        { "startBtn", "START" },
        { "failed", "Failed" },
        { "restartBtn", "RESTART" },
        { "passed", "Passed" },
        { "continueBtn", "CONTINUE" },
        { "level", "Level: " },
        { "cars", "Cars: " },
        { "settings", "Settings" },
        { "saveBtn", "Save" },
        { "stayBtn", "Stay" },
        { "watchAdBtn", "Save record\nfor Ads" }
    };

    Dictionary<string, string> ruTranslation = new Dictionary<string, string>
        {
            { "logo", "���� �� ��������" },
            { "startBtn", "������" },
            { "failed", "������" },
            { "restartBtn", "������" },
            { "passed", "��������" },
            { "continueBtn", "����������" },
            { "level", "�������: " },
            { "cars", "������: " },
            { "settings", "���������" },
            { "saveBtn", "���������" },
            { "stayBtn", "��������" },
            { "watchAdBtn", "��������� ������\n�� �������" }
        };


    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (ManagerLocalization.Instance == null) return;

        switch (ManagerLocalization.Instance.Language)
        {
            case LanguageType.English:
                GetComponent<TextMeshProUGUI>().text = enTranslation[translationKey];
                break;
            case LanguageType.Russian:
                GetComponent<TextMeshProUGUI>().text = ruTranslation[translationKey];
                break;
        }
    }
}