using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Collider))]
public class NPCDialogueZone : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject PressObj;
    [SerializeField] GameObject NPCGameObj;

    private void OnTriggerEnter(Collider other)
    {
        if (PressObj != null)
        {
            PressObj.SetActive(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //NPCGameObj.transform.DORotate(other.transform.position, 1);
            if (Input.GetKey(KeyCode.F))
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (PressObj != null)
        {
            PressObj.SetActive(false);
        }
    }
}

