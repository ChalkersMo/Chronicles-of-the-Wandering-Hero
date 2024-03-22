using UnityEngine;

public class UiBilboarding : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        transform.forward = cam.transform.forward;
    }
}
