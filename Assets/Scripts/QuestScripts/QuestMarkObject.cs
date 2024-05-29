using UnityEngine;

public class QuestMarkObject : MonoBehaviour
{
    [SerializeField] private QuestScriptable questScriptable;
    [SerializeField] private QuestPhaseScriptable phaseScriptable;
    [SerializeField] private bool isPhase;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.CheckPointPosition  = transform.position;

        if (isPhase)
            phaseScriptable.QuestPhaseMark = gameObject;
        else
            questScriptable.QuestMark = gameObject;

        gameObject.SetActive(false);
    }
}
