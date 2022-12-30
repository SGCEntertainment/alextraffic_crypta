using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;

    private void Start()
    {
        Ball.OnCollided += () =>
        {
            if (sfxSource.isPlaying)
            {
                sfxSource.Stop();
            }

            sfxSource.Play();
        };
    }
}
