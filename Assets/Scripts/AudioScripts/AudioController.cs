using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] fightingAudioClips;

    public bool IsFightingTheme;

    private AudioSource audioSource;
    private AudioClip tempAudioClip;

    public AudioClip previousAudioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeTheme(AudioClip clip, float duration, bool isFightingTheme)
    {
        StartCoroutine(ChangingTheme(clip, duration, isFightingTheme));
    }
    private IEnumerator ChangingTheme(AudioClip clip, float duration, bool isFightingTheme)
    {
        if (!isFightingTheme)
        {
            if (tempAudioClip != null)
            {
                previousAudioClip = audioSource.clip;
                audioSource.DOFade(0, duration);
                yield return new WaitForSeconds(duration);
                audioSource.clip = clip;
                tempAudioClip = clip;
                audioSource.DOFade(1, duration);
                audioSource.Play();
            }
            else
            {
                previousAudioClip = audioSource.clip;
                audioSource.clip = clip;
                tempAudioClip = clip;
                audioSource.DOFade(1, duration);
                audioSource.Play();
            }
        }
        else
        {
            if (tempAudioClip != null)
            {
                previousAudioClip = audioSource.clip;
                int randomClip = Random.Range(0, fightingAudioClips.Length);
                audioSource.DOFade(0, duration);
                yield return new WaitForSeconds(duration);
                audioSource.clip = fightingAudioClips[randomClip];
                tempAudioClip = fightingAudioClips[randomClip];
                audioSource.DOFade(1, duration);
                audioSource.Play();
            }
            else
            {
                previousAudioClip = audioSource.clip;
                int randomClip = Random.Range(0, fightingAudioClips.Length);
                audioSource.clip = fightingAudioClips[randomClip];
                tempAudioClip = fightingAudioClips[randomClip];
                audioSource.DOFade(1, duration);
                audioSource.Play();
            }
            IsFightingTheme = true;
        }
       
    }
}
