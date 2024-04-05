using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FirstQuest : MonoBehaviour
{
    int kills;
    public UnityEvent killcounter;

    private void Start()
    {
        // killcounter.AddListener(GameObject.FindGameObjectWithTag(""));
    }
    public void questProgres()
    {
        kills++;
        Debug.Log("questCompletted");

        if (kills == 3)
        {
            Debug.Log("questCompletted");
            questCompletted();
        }
    }
    void questCompletted()
    {

    }
}
