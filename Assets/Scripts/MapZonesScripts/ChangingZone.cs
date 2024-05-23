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

    private Volume globalVolume;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    private void Start()
    {
        globalVolume =  GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        nameText = rootNamingZoneObj.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = rootNamingZoneObj.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VignetteSetUp(zoneScriptable.VignetteColor,
                zoneScriptable.VignetteIntensity,
                zoneScriptable.VignetteSmoothness);

            EnteringTextAnim(zoneScriptable.Name, zoneScriptable.Description);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VignetteSetUp(defaultZoneScriptable.VignetteColor,
                defaultZoneScriptable.VignetteIntensity,
                defaultZoneScriptable.VignetteSmoothness);
        }
    }

    private void VignetteSetUp(Color color, float Intensity, float Smoothness)
    {
        if(globalVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.color.value = color;
            vignette.intensity.value = Intensity;
            vignette.smoothness.value = Smoothness;
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
