using UnityEngine;

public class StayInsideMiniMap : MonoBehaviour
{
    [SerializeField] private Transform MinimapCam;
    private float MinimapSize = 13;

    private Vector3 TempV3;

    void Update()
    {
        Vector3 parentPos = transform.parent.transform.parent.transform.position;
        TempV3 = parentPos;
        TempV3.y = parentPos.y + 1;
        transform.position = TempV3;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MinimapCam.position.x - MinimapSize, MinimapSize + MinimapCam.position.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, MinimapCam.position.z - MinimapSize, MinimapSize + MinimapCam.position.z)
        );
    }
}
