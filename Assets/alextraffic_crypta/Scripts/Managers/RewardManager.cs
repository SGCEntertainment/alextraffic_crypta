using UnityEngine.UI;
using UnityEngine;
using System;

public class RewardManager : MonoBehaviour
{
    private int Last
    {
        get => PlayerPrefs.GetInt("last", 0);
        set => PlayerPrefs.SetInt("last", value);
    }

    private int Count
    {
        get => PlayerPrefs.GetInt("count", 0);
        set => PlayerPrefs.SetInt("count", value);
    }

    [SerializeField] Button acceptRewardBtn;
    [SerializeField] GameObject rewardPopup;

    [Space(10)]
    [SerializeField] int[] rewards;

    public void Check()
    {
        DateTime now = DateTime.Now;

        if (now.Day != Last)
        {
            rewardPopup.SetActive(true);
            acceptRewardBtn.onClick.AddListener(() =>
            {
                Balance.Instance.UpdateBalance(rewards[Count]);

                Last = now.Day;

                Count++;
                if (Count > rewards.Length)
                {
                    Count = 0;
                }

                rewardPopup.SetActive(false);
            });
        }
    }
}
