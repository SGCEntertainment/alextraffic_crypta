using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] ScrollRect scrollRect;

    [Space(10)]
    [SerializeField] GameObject popup;

    [Space(10)]
    [SerializeField] GameObject[] pages;

    private void Awake()
    {
        Navigation.OnPageSelected += (_name, id) =>
        {
            titleText.text = _name;
            for(int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(i == id);
            }

            scrollRect.content = pages[id].GetComponent<RectTransform>();
        };
    }

    public void ShowPopup(bool IsOpened)
    {
        popup.SetActive(IsOpened);
    }
}
