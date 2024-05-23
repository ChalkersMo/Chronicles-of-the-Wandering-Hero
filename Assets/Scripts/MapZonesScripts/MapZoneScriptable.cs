using UnityEngine;

[CreateAssetMenu(fileName = "NewZone", menuName = "MapZone")]
public class MapZoneScriptable : ScriptableObject
{
    [Header("Vignette settings")]
    public Color VignetteColor = Color.black;

    public float VignetteIntensity = 0.35f;
    public float VignetteSmoothness = 1;

    [Space, Header("Zone naming")]
    public string Name;
    public string Description;
}
