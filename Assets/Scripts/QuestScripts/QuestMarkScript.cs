using System;
using TMPro;
using UnityEngine;

public class QuestMarkScript : MonoBehaviour
{
    private GameObject _tempMark;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void ShowQuestMark(QuestScriptable quest)
    {
        if (quest.QuestMark != null && _tempMark != null)
        {
            _tempMark.gameObject.SetActive(false);
            _tempMark = quest.QuestMark;
            _tempMark.gameObject.SetActive(true);
        }
        else if (quest.QuestMark != null && _tempMark == null)
        {
            _tempMark = quest.QuestMark;
            _tempMark.gameObject.SetActive(true);
        }
        else if (quest.QuestMark == null && _tempMark != null)
        {
            _tempMark.gameObject.SetActive(false);
            _tempMark = null;
        }
    }

    public void ShowQuestPhaseMark(QuestPhaseScriptable questPhase)
    {
        if (questPhase.QuestPhaseMark != null && _tempMark != null)
        {
            _tempMark.gameObject.SetActive(false);
            _tempMark = questPhase.QuestPhaseMark;
            _tempMark.gameObject.SetActive(true);
        }
        else if(questPhase.QuestPhaseMark != null && _tempMark == null)
        {
            _tempMark = questPhase.QuestPhaseMark;
            _tempMark.gameObject.SetActive(true);
        }
        else if (questPhase.QuestPhaseMark == null && _tempMark != null)
        {
            _tempMark.gameObject.SetActive(false);
            _tempMark = null;
        }

        if (_tempMark != null && _tempMark.transform.childCount > 1)
        {
            for(int i = 0; i <= _tempMark.transform.childCount; i++)
            {
                if (i > 0)
                    _tempMark.transform.GetChild(i).transform.SetParent(null);
            }
        }
    }

    private void Update()
    {
        if (_tempMark != null)
        {
            float distance = Vector3.Distance(_player.position, _tempMark.transform.position);
            _tempMark.GetComponentInChildren<TextMeshProUGUI>().text = $"{Math.Round(distance)}m";
        }
    }
}
