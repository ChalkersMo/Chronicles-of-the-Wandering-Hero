using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(Collider))]
public class NPCDialogueZone : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject NPCGameObj;
    [SerializeField] GameObject PressButtonTip;

    private void OnTriggerEnter(Collider other)
    {
        if (PressButtonTip != null)
        {
            PressButtonTip.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //NPCGameObj.transform.DORotate(other.transform.position, 1);
            if (Input.GetKey(KeyCode.F))
            {
                PlayerController PC = FindObjectOfType<PlayerController>();
                PlayerSwordAnimationController PSAC = FindObjectOfType<PlayerSwordAnimationController>();
                if (PressButtonTip != null)
                {
                    PressButtonTip.SetActive(false);
                }
                PC.enabled = false;
                PSAC.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);               
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (PressButtonTip != null)
        {
            PressButtonTip.SetActive(false);
        }
    }
}

