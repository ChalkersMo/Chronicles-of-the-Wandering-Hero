using UnityEngine;

[CreateAssetMenu(fileName = "NewZone", menuName = "MapZone")]
public class MapZoneScriptable : ScriptableObject
{
    [Header("Zone naming")]
    public string Name;
    public string Description;

    [Space, Header("Vignette settings")]
    public Color VignetteColor = Color.black;

    public float VignetteIntensity = 0.35f;
    public float VignetteSmoothness = 1;

    public float ChangingDuration = 3f;

    [Space, Header("Lift Gamma and Gain settings")]
    public float LiftW;
    public float GammaW;
    public float GainW;

    public float Duration = 2f;
}
