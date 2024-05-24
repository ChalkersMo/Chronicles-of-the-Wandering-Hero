using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangingZone : MonoBehaviour
{
    [SerializeField] MapZoneScriptable defaultZoneScriptable;
    [SerializeField] MapZoneScriptable zoneScriptable;

    [SerializeField] Transform rootNamingZoneObj;

    private GlobalVolumeController globalVolumeController;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    private void Start()
    {
        globalVolumeController =  GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<GlobalVolumeController>();
        nameText = rootNamingZoneObj.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = rootNamingZoneObj.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            globalVolumeController.ChangeVignette(zoneScriptable.VignetteColor,
                zoneScriptable.VignetteIntensity,
                zoneScriptable.VignetteSmoothness,
                zoneScriptable.ChangingDuration);

            globalVolumeController.ChangeGamma(zoneScriptable.GammaW, zoneScriptable.Duration);
            globalVolumeController.ChangeGain(zoneScriptable.GainW, zoneScriptable.Duration);

            EnteringTextAnim(zoneScriptable.Name, zoneScriptable.Description);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            globalVolumeController.ChangeVignette(defaultZoneScriptable.VignetteColor,
                defaultZoneScriptable.VignetteIntensity,
                defaultZoneScriptable.VignetteSmoothness,
                defaultZoneScriptable.ChangingDuration);

            globalVolumeController.ChangeGamma(defaultZoneScriptable.GammaW, defaultZoneScriptable.Duration);
            globalVolumeController.ChangeGain(defaultZoneScriptable.GainW, defaultZoneScriptable.Duration);
        }
    }

    private void EnteringTextAnim(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;

        nameText.DOFade(1, 0.4f);
        descriptionText.DOFade(1, 0.4f);

        rootNamingZoneObj.DOLocalMoveY(440, 1.5f);
        Invoke(nameof(TextAnimOut), 3);
    }

    private void TextAnimOut()
    {
        rootNamingZoneObj.DOLocalMoveY(600, 1f);

        nameText.DOFade(0, 0.4f);
        descriptionText.DOFade(0, 0.4f);
    }
}
