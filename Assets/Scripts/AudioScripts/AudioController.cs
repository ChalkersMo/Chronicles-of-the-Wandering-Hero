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

    public void ChangeTheme(AudioClip clip, float duration, bool isFightingTheme, float volume)
    {
        StartCoroutine(ChangingTheme(clip, duration, isFightingTheme, volume));
    }
    private IEnumerator ChangingTheme(AudioClip clip, float duration, bool isFightingTheme, float volume)
    {
        if (!isFightingTheme)
        {
            if (tempAudioClip != null)
            {
                previousAudioClip = audioSource.clip;
                audioSource.DOFade(volume, duration);
                yield return new WaitForSeconds(duration);
                audioSource.clip = clip;
                tempAudioClip = clip;
                audioSource.DOFade(volume, duration);
                audioSource.Play();
            }
            else
            {
                previousAudioClip = audioSource.clip;
                audioSource.clip = clip;
                tempAudioClip = clip;
                audioSource.DOFade(volume, duration);
                audioSource.Play();
            }
        }
        else
        {
            if (tempAudioClip != null)
            {
                previousAudioClip = audioSource.clip;
                int randomClip = Random.Range(0, fightingAudioClips.Length);
                audioSource.DOFade(volume, duration);
                yield return new WaitForSeconds(duration);
                audioSource.clip = fightingAudioClips[randomClip];
                tempAudioClip = fightingAudioClips[randomClip];
                audioSource.DOFade(volume, duration);
                audioSource.Play();
            }
            else
            {
                previousAudioClip = audioSource.clip;
                int randomClip = Random.Range(0, fightingAudioClips.Length);
                audioSource.clip = fightingAudioClips[randomClip];
                tempAudioClip = fightingAudioClips[randomClip];
                audioSource.DOFade(volume, duration);
                audioSource.Play();
            }
            IsFightingTheme = true;
        }
       
    }
}
