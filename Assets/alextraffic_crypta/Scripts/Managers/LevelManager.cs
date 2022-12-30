using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int currentKicks;
    public static int levelIndex;

    [SerializeField] Color active;

    [Space(10)]
    [SerializeField] int[] kicks;
    [SerializeField] Image[] levels;

    private void Start()
    {
        SetLevel(0);
    }

    public void SetLevel(int _levelIndex)
    {
        currentKicks = kicks[_levelIndex];
        for(int i = 0; i < levels.Length; i++)
        {
            levels[i].color = levels[i].transform.GetSiblingIndex() == _levelIndex ? active : Color.white;
        }

        levelIndex = _levelIndex;
    }

    public void NextLevel()
    {
        levelIndex++;
        if(levelIndex > levels.Length - 1)
        {
            levelIndex = levels.Length - 1;
        }

        currentKicks = kicks[levelIndex];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].color = levels[i].transform.GetSiblingIndex() == levelIndex ? active : Color.white;
        }
    }
}
