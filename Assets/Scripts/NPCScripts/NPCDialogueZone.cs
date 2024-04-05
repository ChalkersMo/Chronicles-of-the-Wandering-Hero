using UnityEngine;
using DG.Tweening;
using TMPro;
[RequireComponent(typeof(Collider))]
public class NPCDialogueZone : MonoBehaviour
{
    Collider _collider;
    [SerializeField] NPC NPCScriptableObject;
    [SerializeField] GameObject NPCGameObj;
    [SerializeField] GameObject PressButtonTip;
    [SerializeField] GameObject NameTextGO;
    TextMeshProUGUI _nameText;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
        Vector3 healthBarPos = new Vector3(transform.position.x, colliderTop + 0.5f, transform.position.z);
        NameTextGO = Instantiate(NameTextGO, healthBarPos, Quaternion.identity, transform);
        _nameText = NameTextGO.GetComponentInChildren<TextMeshProUGUI>();
        _nameText.text = $"{NPCScriptableObject.Name}";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PressButtonTip != null)
            {
                PressButtonTip.SetActive(true);
            }
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
                if (PressButtonTip != null)
                    PressButtonTip.SetActive(false);

                PC.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                FindObjectOfType<DialogueManager>().StartDialogue(NPCScriptableObject);               
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PressButtonTip != null)
            {
                PressButtonTip.SetActive(false);
            }
        }
    }
}

