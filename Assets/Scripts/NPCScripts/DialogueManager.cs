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
    public bool IsDialogueEnd = false;
    [SerializeField] GameObject TPC;
    public static event Action EndOfDialogue;

    void Start()
    {
        sentences = new Queue<string>();
        DialogueCanvas.SetActive(false);
    }
    public void StartDialogue(NPC dialogue)
    {
        TPC.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        IsDialogueEnd = false;
        DialogueCanvas.SetActive(true);
        nametext.text = dialogue.Name;
        sentences.Clear();
        if(dialogue.isTaskCompleted != true)
        {
            foreach (string sentence in dialogue.sentences1)
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            foreach (string sentence in dialogue.sentences2)
            {
                sentences.Enqueue(sentence);
            }
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
        StopAllCoroutines();
        PlayerController PC = FindObjectOfType<PlayerController>();
        PC.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        IsDialogueEnd = true;
        DialogueCanvas.SetActive(false);
        TPC.SetActive(true);
        EndOfDialogue?.Invoke();
    }
}
