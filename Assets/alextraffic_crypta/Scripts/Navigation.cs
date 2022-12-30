using UnityEngine.UI;
using UnityEngine;
using System;

public class Navigation : MonoBehaviour
{
    private Button[] NavButtons { get; set; }
    private Image[] NavImages { get; set; }
    private Text[] NavTexts { get; set; }

    [SerializeField] Color activeColor;

    public static Action<string> OnPageSelected { get; set; }

    private void Awake()
    {
        NavButtons = new Button[transform.childCount];

        NavImages = new Image[transform.childCount];
        NavTexts = new Text[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            NavButtons[i] = transform.GetChild(i).GetComponent<Button>();

            NavImages[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
            NavTexts[i] = transform.GetChild(i).GetChild(1).GetComponent<Text>();
        }

        SetPage(0);
    }

    public void SetPage(int id)
    {
        for (int i = 0; i < NavButtons.Length; i++)
        {
            NavImages[i].color = i == id ? activeColor : Color.black;
            NavTexts[i].color = i == id ? activeColor : Color.black;

            OnPageSelected?.Invoke(NavTexts[id].text);
        }
    }
}
