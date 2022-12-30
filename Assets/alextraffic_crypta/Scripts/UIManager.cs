using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text titleText;

    private void Awake()
    {
        Navigation.OnPageSelected += (_name) =>
        {
            titleText.text = _name;
        };
    }
}
