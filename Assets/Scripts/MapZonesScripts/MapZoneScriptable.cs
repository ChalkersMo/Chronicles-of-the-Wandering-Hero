using UnityEngine;

[CreateAssetMenu(fileName = "NewZone", menuName = "MapZone")]
public class MapZoneScriptable : ScriptableObject
{
    [Header("Zone naming")]
    public string Name;
    public string Description;

    [Space, Header("Music settings")]
    public AudioClip ZoneThemeClip;
    public float ClipChangingDuration = 2;
    public float ClipVolume = 0.7f;

    [Space, Header("Vignette settings")]
    public Color VignetteColor = Color.black;

    public float VignetteIntensity = 0.35f;
    public float VignetteSmoothness = 1;

    public float VigneteChangingDuration = 3f;

    [Space, Header("Lift Gamma and Gain settings")]
    public float LiftW = 0;
    public float GammaW = -0.1f;
    public float GainW = 0.45f;

    public float LiftGammaGainChangingDuration = 2f;
}
