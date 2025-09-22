using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ManagerMenu : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource buttonClick;
    [SerializeField] AudioSource lose;
    [SerializeField] AudioSource levelPassed;

    [Header("UI Set")]
    [SerializeField] List<ActionUI> startActionUI;
    [SerializeField] List<ActionUI> lostActionUI;
    [SerializeField] List<ActionUI> winActionUI;

    [SerializeField] GameObject AdsBtn;

    public bool IsLost { get; private set; } = false;
    public static bool SaveLevel { get; set; } = false;

    string mainSceneName = "MainScene";
    ManagerCars managerCars;

    private void Start()
    {
        managerCars = FindFirstObjectByType<ManagerCars>();
    }

    public void StartGame()
    {
        ManagerCars.IsStarted = true;
        managerCars.DismissalStartCar();

        foreach (ActionUI ui in startActionUI)
            ui.StartActionUI();

    }

    public void LevelPassed()
    {
        levelPassed.Play();
        foreach (ActionUI ui in winActionUI)
            ui.StartActionUI();
    }

    public void LostGame()
    {
        if (IsLost) return;
        IsLost = true;

        lose.Play();
        foreach (ActionUI ui in lostActionUI)
            ui.StartActionUI();
    }

    public void Restart()
    {
        YG2.saves.soundEnabled = ManagerToggle.SoundOn;

        if (IsLost && !SaveLevel)
        {
            YG2.saves.level = 1;
        }

        SaveLevel = false;
        Adv.Instance.CountLoad();
        YG2.SaveProgress();
        SceneManager.LoadScene(SceneManager.GetSceneByName(mainSceneName).buildIndex);
    }

    public void Continue()
    {
        YG2.saves.level = ManagerCars.CurrentLevel + 1;
        Restart();
    }

    public void SaveRecordForAds()
    {
        Adv.Instance.ShowRewardAdv();
        AdsBtn.SetActive(false);
    }

    public void Save() => YG2.SaveProgress();
}
