using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public AudioClip walkingClip;
    public AudioClip runningClip;

    [SerializeField] private AudioClip pickingUp;
    [SerializeField] private AudioClip jumpingClip;
    [SerializeField] private AudioClip rollingClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip slashClip;

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
        audioSource.clip = rollingClip;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayDeathSound()
    {
        audioSource.clip = deathClip;
        audioSource.Play();
        somethingPlaying = true;
    }
    public void PlaySlashSound()
    {
        audioSource.clip = slashClip;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayPickUpSound()
    {
        audioSource.clip = pickingUp;
        audioSource.Play();
        somethingPlaying = true;
    }
}
