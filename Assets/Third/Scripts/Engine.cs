using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

public class Engine : MonoBehaviour
{
    UniWebView View { get; set; }

    bool servicesInitialized;
	string target;

	delegate void FinalActionHandler(string campaign);
	event FinalActionHandler OnFinalActionEvent;

	public struct UserAttributes { }
	public struct AppAttributes { }

	private void OnEnable()
	{
		OnFinalActionEvent += Engine_OnFinalActionEvent;
	}

	private void OnDisable()
	{
		OnFinalActionEvent -= Engine_OnFinalActionEvent;
	}

	private void Engine_OnFinalActionEvent(string campaign)
	{
		if (string.IsNullOrEmpty(campaign) || string.IsNullOrWhiteSpace(campaign))
		{
			Screen.fullScreen = true;
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
		else
		{
			Init(campaign);
		}
	}

	async Task Awake()
	{
		if (Utilities.CheckForInternetConnection())
		{
			await InitializeRemoteConfigAsync();
		}

		RemoteConfigService.Instance.FetchCompleted += (responce) =>
		{
            bool enable = RemoteConfigService.Instance.appConfig.GetBool("enable");
            if (!enable)
            {
                OnFinalActionEvent?.Invoke(string.Empty);
            }

            servicesInitialized = true;
        };

		await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
		servicesInitialized = true;
	}

	async Task InitializeRemoteConfigAsync()
	{
		// initialize handlers for unity game services
		await UnityServices.InitializeAsync();

		// remote config requires authentication for managing environment information
		if (!AuthenticationService.Instance.IsSignedIn)
		{
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
	}

	IEnumerator Start()
	{
		CacheComponents();
		while (!servicesInitialized)
		{
			yield return null;
		}
	}

	void CacheComponents()
	{
        View = gameObject.AddComponent<UniWebView>();
        Camera.main.backgroundColor = Color.black;

        View.ReferenceRectTransform  = GameObject.Find("rect").GetComponent<RectTransform>();

        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        View.ReferenceRectTransform.anchorMin = anchorMin;
        View.ReferenceRectTransform.anchorMax = anchorMax;

        View.SetShowSpinnerWhileLoading(false);
        View.BackgroundColor = Color.white;

        View.OnOrientationChanged += (v, o) =>
        {
			Screen.fullScreen = o == ScreenOrientation.Landscape;

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            v.ReferenceRectTransform.anchorMin = anchorMin;
            v.ReferenceRectTransform.anchorMax = anchorMax;

            View.UpdateFrame();
        };

        View.OnShouldClose += (v) =>
        {
            return false;
        };

        View.OnPageStarted += (browser, url) =>
        {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            View.ReferenceRectTransform.anchorMin = anchorMin;
            View.ReferenceRectTransform.anchorMax = anchorMax;

            View.Show();
            View.UpdateFrame();
        };

        View.OnPageFinished += (browser, code, url) =>
        {
			GameObject.Find("promo").SetActive(false);
        };
    }

	void Init(string campaign)
	{
		new GameObject("Manager").AddComponent<Flipmorris.Manager>();

        GameObject.Find("spinner").SetActive(false);
        GameObject.Find("appIcon").SetActive(false);
        GameObject.Find("loadingText").SetActive(false);

		target = "";
        View.Load(target);
    }
}
