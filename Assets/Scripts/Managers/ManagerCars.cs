using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;
using YG;

public class ManagerCars : MonoBehaviour
{
    [SerializeField] GameObject startCar;
    [SerializeField] string colorDigit;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI countCars;

    string pattern = "<color={0}>{1}</color>";
    string carRoadTag = "CarRoad";
    string startCarTag= "StartCar";

    bool levelPassed = false;
    ManagerMenu managerMenu;
    SpawnerCarParking spawnerCarParking;

    public static int CurrentLevel { get; private set; }
    public static bool IsStarted { get; set; } = false;
    public static int CountCarsParking { get; set; }

    void Awake()
    {
        spawnerCarParking = FindFirstObjectByType<SpawnerCarParking>();

        SpawnerRoadCar.IsSpawning = true;
        startCar.tag = startCarTag;
    }

    void Start()
    {
        CurrentLevel = YG2.saves.level;
        CountCarsParking = CurrentLevel + 4;
        if (spawnerCarParking != null && CountCarsParking > spawnerCarParking.MaxCountCar)
            CountCarsParking = spawnerCarParking.MaxCountCar;

        spawnerCarParking.SpawnCars();

        managerMenu = FindFirstObjectByType<ManagerMenu>();

        level.text = string.Format(pattern, colorDigit, CurrentLevel);

        startCar.GetComponent<CarRoad>().TargetPos = Assets.Scripts.TargetPosition.Position_1;
    }

    void Update()
    {
        countCars.text = string.Format(pattern, colorDigit, CountCarsParking);
        if (CountCarsParking == 0 && !levelPassed)
        {
            levelPassed = true;
            managerMenu.LevelPassed();
        }
    }

    public void DismissalStartCar()
    {
        startCar.tag = carRoadTag;
    }
}
