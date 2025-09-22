using YG;
using UnityEngine;

public class Adv : MonoBehaviour
{
    public static Adv Instance;

    string rewardID = "123";
    int countLoad;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            YG2.StickyAdActivity(true);
        }
        else
            Destroy(gameObject);
    }

    public void CountLoad()
    {
        countLoad++;
        if (countLoad == 4)
        {
            countLoad = 0;
            YG2.InterstitialAdvShow();
        }
    }

    public void ShowRewardAdv()
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            ManagerMenu.SaveLevel = true;
            FindFirstObjectByType<ManagerMenu>().Restart();
        });
    }

}
