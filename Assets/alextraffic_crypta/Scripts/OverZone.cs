using UnityEngine;

public class OverZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.CompareTag("ball") || !GameManager.GameStarted)
        {
            return;
        }

        GameManager.Instance.uiManager.ShowResult(false);
    }
}
