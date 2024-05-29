using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public AudioClip walkingClip;
    public AudioClip runningClip;

    [SerializeField] private AudioClip jumpingClip;
    [SerializeField] private AudioClip RollingClip;

    private bool somethingPlaying = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RenewSource()
    {
        if (somethingPlaying)
        {
            audioSource.clip = null;
            audioSource.loop = false;
            audioSource.pitch = 1;
            audioSource.Stop();
            somethingPlaying = false;
        }
    }

    public void PlayWalkingSound()
    {
        audioSource.clip = walkingClip;
        audioSource.loop = true;
        audioSource.pitch = 0.5f;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayRunningSound()
    {
        audioSource.clip = runningClip;
        audioSource.loop = true;
        audioSource.pitch = 0.8f;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayJumpSound()
    {
        audioSource.clip = jumpingClip;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayRollSound()
    {
        audioSource.clip = RollingClip;
        audioSource.Play();
        somethingPlaying = true;
    }
}
