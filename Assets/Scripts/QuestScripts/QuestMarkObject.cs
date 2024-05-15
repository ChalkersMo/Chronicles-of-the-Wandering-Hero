using UnityEngine;

public class QuestMarkObject : MonoBehaviour
{
    [SerializeField] QuestScriptable questScriptable;
    [SerializeField] QuestPhaseScriptable phaseScriptable;
    [SerializeField] bool isPhase;

    private void Start()
    {
        gameObject.SetActive(false);
        if (isPhase)
            phaseScriptable.QuestPhaseMark = gameObject;
        else
            questScriptable.QuestMark = gameObject;
    }
}
