using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Action OnCollided { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(SettingsManager.VibraEnable)
        {
            Handheld.Vibrate();
        }

        if(!collision.collider.CompareTag("foot") || !GameManager.GameStarted)
        {
            return;
        }

        OnCollided?.Invoke();
    }
}
