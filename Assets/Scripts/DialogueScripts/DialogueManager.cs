using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Fields for dialogue panel")]
    [SerializeField] private Image characterIcon;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Space]
    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    [SerializeField] private GameObject DialogueCanvas;

    [SerializeField] GameObject TPC;
    public static event Action EndOfDialogue;

    [SerializeField] private PlayerController playerController;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerController = FindObjectOfType<PlayerController>();
        lines = new Queue<DialogueLine>();
        DialogueCanvas.transform.DOScale(0, 0);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        TPC.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        
        DialogueCanvas.transform.DOScale(1, 1);
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueText.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        playerController.enabled = true;
        TPC.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        DialogueCanvas.transform.DOScale(0, 1);
        EndOfDialogue?.Invoke();
        EndOfDialogue = null;
    }
}
