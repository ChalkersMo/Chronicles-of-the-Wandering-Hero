using DG.Tweening;
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
        DialogueCanvas.transform.DOScale(0, 0);
    }
    public void StartDialogue(NPC NPCScriptable)
    {
        TPC.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        IsDialogueEnd = false;
        DialogueCanvas.transform.DOScale(1, 1);
        nametext.text = NPCScriptable.Name;
        sentences.Clear();
        if(NPCScriptable.isTaskCompleted != true)
        {
            foreach (string sentence in NPCScriptable.sentences1)
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            foreach (string sentence in NPCScriptable.sentences2)
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
        DialogueCanvas.transform.DOScale(0, 1);
        TPC.SetActive(true);
        EndOfDialogue?.Invoke();
        EndOfDialogue = null;
    }
}
