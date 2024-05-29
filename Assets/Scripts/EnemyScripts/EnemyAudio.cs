using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip runningClip;
    [SerializeField] private AudioClip attackingClip;
    [SerializeField] private AudioClip uniqueAttackingClip;
    [SerializeField] private AudioClip takedamageClip;

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
        if (audioSource.clip != walkingClip)
        {
            audioSource.clip = walkingClip;
            audioSource.loop = true;
            audioSource.pitch = 0.5f;
            audioSource.Play();
            somethingPlaying = true;
        }
    }

    public void PlayRunningSound()
    {
        if(audioSource.clip != runningClip)
        {
            audioSource.clip = runningClip;
            audioSource.loop = true;
            audioSource.pitch = 0.8f;
            audioSource.Play();
            somethingPlaying = true;
        }      
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackingClip;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayUniqueAttackSound()
    {
        audioSource.clip = uniqueAttackingClip;
        audioSource.Play();
        somethingPlaying = true;
    }

    public void PlayTakingdamageSound()
    {
        audioSource.clip = takedamageClip;
        audioSource.Play();
        somethingPlaying = true;
    }
}
