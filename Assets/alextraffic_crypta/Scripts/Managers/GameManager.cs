using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => FindObjectOfType<GameManager>(); }

    public static bool GameStarted { get; set; }

    private GameObject PlayerRef { get; set; }
    private GameObject GroundRef { get; set; }
    private GameObject BallRef { get; set; }


    private GameObject PlayerPrefab { get; set; }
    private GameObject GroundPrefab { get; set; }
    private GameObject BallPrefab { get; set; }

    private Transform EnvironmentRef { get; set; }

    private const int initTime = 300;
    public float ElapsedSeconds { get; private set; }

    public UIManager uiManager;

    private void Awake()
    {
        PlayerPrefab = Resources.Load<GameObject>("player");
        GroundPrefab = Resources.Load<GameObject>("ground");
        BallPrefab = Resources.Load<GameObject>("ball");

        EnvironmentRef = GameObject.Find("Environment").transform;
    }

    public void DestroyOld()
    {
        Destroy(PlayerRef);
        Destroy(GroundRef);
        Destroy(BallRef);
    }

    public void StartGame()
    {
        ElapsedSeconds = initTime;

        DestroyOld();

        PlayerRef = Instantiate(PlayerPrefab, EnvironmentRef);
        GroundRef = Instantiate(GroundPrefab, EnvironmentRef);
        BallRef = Instantiate(BallPrefab, EnvironmentRef);

        GameStarted = true;
    }

    public void EndGame()
    {
        DestroyOld();
        uiManager.OpenWindow(5);

        GameStarted = false;
    }
}