using UnityEngine;

public class CanvasesSortingOrder : MonoBehaviour
{
    public static CanvasesSortingOrder Instance;

    private Canvas[] canvases;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        canvases = FindObjectsOfType<Canvas>();
    }
    public void ShowOnFirst(Canvas canvas)
    {
        foreach (Canvas _canvas in canvases)
            _canvas.sortingOrder = 0;
        canvas.sortingOrder = 1;
    }
}
