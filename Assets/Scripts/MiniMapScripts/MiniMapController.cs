using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    private Transform _player;
    private Camera _camera;

    [SerializeField] private float defaultOrthographicSize = 15;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 newPos = _player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;   

        transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0f);
    }

    public void Zoom(float zoom)
    {
        _camera.orthographicSize = zoom;
    }

    public void ZoomOut()
    {
        _camera.orthographicSize = defaultOrthographicSize;
    }
}
