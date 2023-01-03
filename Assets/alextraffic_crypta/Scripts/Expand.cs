using UnityEngine.UI;
using UnityEngine;

public class Expand : MonoBehaviour
{
    private bool IsExpand { get; set; }
    private Transform ExpandTransform { get; set; }

    private void Awake()
    {
        ExpandTransform = transform.GetChild(1);

        IsExpand = false;
        ExpandTransform.gameObject.SetActive(IsExpand);

        GetComponent<Button>().onClick.AddListener(() =>
        {
            IsExpand = !IsExpand;
            ExpandTransform.gameObject.SetActive(IsExpand);
        });
    }
}
