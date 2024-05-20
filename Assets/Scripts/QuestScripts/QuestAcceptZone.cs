using UnityEngine;

public class QuestAcceptZone : MonoBehaviour
{
    [SerializeField, Header("If need to activate quest put it here," +
        " if need to progress quest add QuestProgresser component and set it")]
    private QuestScriptable quest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TryGetComponent(out QuestProgresser questProgresser))
            {
                questProgresser.ProgressQuest();
            }

            if (quest != null)
            {
                if(quest.dialogue != null)
                {
                    DialogueManager.EndOfDialogue += QuestDialogue;
                    DialogueManager.Instance.StartDialogue(quest.dialogue);
                }
            }
            Destroy(gameObject);
        }      
    }

    private void QuestDialogue()
    {
        QuestHolder.Instance.QuestAccept(quest, quest.Name);
    }
}
