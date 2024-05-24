using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeController : MonoBehaviour
{
    private Volume globalVolume;
    private Vignette _vignette;
    private LiftGammaGain _liftGammaGain;

    private void Start()
    {
        globalVolume = GetComponent<Volume>();
        if (globalVolume.profile.TryGet(out Vignette vignette))
        {
            _vignette = vignette;
        }
        if (globalVolume.profile.TryGet(out LiftGammaGain liftGammaGain))
        {
            _liftGammaGain = liftGammaGain;
        }
    }

    public void ChangeVignette(Color color, float Intensity, float Smoothness, float ChangingDuration)
    {
        StartCoroutine(VignetteSetUp(color, Intensity, Smoothness, ChangingDuration));
    }
    private IEnumerator VignetteSetUp(Color color, float Intensity, float Smoothness, float ChangingDuration)
    {
        Intensity = Mathf.Clamp(Intensity, 0f, 1f);
        Smoothness = Mathf.Clamp(Smoothness, 0f, 1f);

        float elapsed = 0.0f;
        float tempIntensity = _vignette.intensity.value;
        float tempShoothness = _vignette.smoothness.value;
        Color tempColor = _vignette.color.value;

        while (elapsed < ChangingDuration)
        {
            _vignette.intensity.value = Mathf.Lerp(tempIntensity, Intensity, elapsed / ChangingDuration);
            _vignette.smoothness.value = Mathf.Lerp(tempShoothness, Smoothness, elapsed / ChangingDuration);
            _vignette.color.value = Color.Lerp(tempColor, color, elapsed / ChangingDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }
        _vignette.intensity.value = Intensity;
        _vignette.smoothness.value = Smoothness;
    }
    public void SaveVignette()
    {

    }

    public void ChangeLift(float Lift, float Duration)
    {
        StartCoroutine(SetUpLift(Lift, Duration));
    }
    public IEnumerator SetUpLift(float Lift, float Duration)
    {
        Vector4 tempLift = _liftGammaGain.lift.value;

        float elapsed = 0.0f;
        while (elapsed < Duration)
        {
            float LiftX = tempLift.x;
            float LiftY = tempLift.y;
            float LiftZ = tempLift.z;
            float LiftW = Mathf.Lerp(tempLift.w, Lift, elapsed / Duration);

            _liftGammaGain.lift.value = new Vector4(LiftX, LiftY, LiftZ, LiftW);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangeGamma(float Gamma, float Duration)
    {
        StartCoroutine(SetUpGamma(Gamma, Duration));
    }
    public IEnumerator SetUpGamma(float Gamma, float Duration)
    {
        Vector4 tempGamma = _liftGammaGain.gamma.value;

        float elapsed = 0.0f;
        while (elapsed < Duration)
        {
            float GammaX = tempGamma.x;
            float GammaY = tempGamma.y;
            float GammaZ = tempGamma.z;
            float GammaW = Mathf.Lerp(tempGamma.w, Gamma, elapsed / Duration);

            _liftGammaGain.gamma.value = new Vector4(GammaX, GammaY, GammaZ, GammaW);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangeGain(float Gain, float Duration)
    {
        StartCoroutine(SetUpGain(Gain, Duration));
    }
    public IEnumerator SetUpGain(float Gain, float Duration)
    {
        Vector4 tempGain = _liftGammaGain.gain.value;

        float elapsed = 0.0f;
        while (elapsed < Duration)
        {
            float GainX = tempGain.x;
            float GainY = tempGain.y;
            float GainZ = tempGain.z;
            float GainW = Mathf.Lerp(tempGain.w, Gain, elapsed / Duration);

            _liftGammaGain.gain.value = new Vector4(GainX, GainY, GainZ, GainW);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
