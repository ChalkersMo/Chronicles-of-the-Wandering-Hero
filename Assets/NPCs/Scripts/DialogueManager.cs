using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueCanvas;
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI dialoguetext;
    private Queue<string> sentences;
    [SerializeField] GameObject Camera;
    public bool IsDialogueEnd = false;

    public static event Action EndOfDialogue;

    void Start()
    {
        sentences = new Queue<string>();
        DialogueCanvas.SetActive(false);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Cursor.lockState = CursorLockMode.None;
        IsDialogueEnd = false;
        DialogueCanvas.SetActive(true);
        Camera.SetActive(false);
        nametext.text = dialogue.Name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentece();
    }

    public void DisplayNextSentece()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialoguetext.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialoguetext.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        IsDialogueEnd = true;
        DialogueCanvas.SetActive(false);
        Camera.SetActive(true);
        EndOfDialogue?.Invoke();
    }
}
