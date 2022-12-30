using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    public static Balance Instance { get => FindObjectOfType<Balance>(); }

    [SerializeField] Text balanceText;

    private int Count
    {
        get => PlayerPrefs.GetInt("balance", 0);
        set => PlayerPrefs.SetInt("balance", value);
    }

    public void UpdateBalance(int amount)
    {
        Count+= amount;
        balanceText.text = Count.ToString();
    }

    private void OnEnable()
    {
        UpdateBalance(0);
    }
}
