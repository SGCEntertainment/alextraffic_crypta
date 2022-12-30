using OneSignalSDK;
using UnityEngine;

public class OneSignalInitializer : MonoBehaviour
{
    private void Start() => OneSignal.Default.Initialize("563bde23-68f2-4681-a793-39f9c81475a0");
}
