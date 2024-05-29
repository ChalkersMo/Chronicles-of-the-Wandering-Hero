using UnityEngine;

public class UiAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip onClip;
    [SerializeField] private AudioClip offClip;

    [Space, Header("Quest sounds")]
    [SerializeField] private AudioClip getQuestClip;
    [SerializeField] private AudioClip progressQuestClip;
    [SerializeField] private AudioClip completeQuestClip;
    [SerializeField] private AudioClip rewardRecieveClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

    public void PlayOnSound()
    {
        audioSource.clip = onClip;
        audioSource.Play();
    }
    public void PlayOffSound()
    {
        audioSource.clip = offClip;
        audioSource.Play();
    }

    public void PlayQuestGettingSound()
    {
        audioSource.clip = getQuestClip;
        audioSource.Play();
    }

    public void PlayCompleteRewardSound()
    {
        audioSource.clip = completeQuestClip;
        audioSource.Play();
    }

    public void PlayRecieveRewardSound()
    {
        audioSource.clip = rewardRecieveClip;
        audioSource.Play();
    }
}
