using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class PlayerLvl : MonoBehaviour
{
    [SerializeField] private Image xpSlider;
    [SerializeField] private TextMeshProUGUI LvlText;
    [SerializeField] private TextMeshProUGUI XpText;

    private PlayerStats playerStats;
    private EventBus eventBus;
    private NPCDialogueZone[] nPCs;

    private int _currentLvl = 1;
    private float _currentXp;
    private float _xpToNextLvl = 250;

    private void Start()
    {
        nPCs = FindObjectsOfType<NPCDialogueZone>();
        playerStats = GetComponent<PlayerStats>();
        eventBus = EventBus.Instance;
        playerStats.CurrentLvl = _currentLvl;
        LvlText.text = _currentLvl.ToString();
        RenewXpImage(_currentXp);
    }
    public void AddXp(float amount)
    {
        _currentXp += amount;
        RenewXpImage(_currentXp);

        if (_currentXp > _xpToNextLvl)
        {
            LvlUpWithDifferenceXp();
        }
        else if(_currentXp == _xpToNextLvl)
        {
            LvlUp();
        }
    }

    private void LvlUp()
    {
        _currentLvl++;
        playerStats.CurrentLvl++;
        _xpToNextLvl *= 2.5f;
        _currentXp = 0;
        RenewXpImage(_currentXp);
        LvlText.text = _currentLvl.ToString();
        foreach (NPCDialogueZone npc in nPCs)
        {
            npc.IsHaveQuest(_currentLvl);
        }

        eventBus.OnLvlUp?.Invoke();
    }

    private void LvlUpWithDifferenceXp()
    {
        float xpDifference = _currentXp - _xpToNextLvl;
        _currentLvl++;
        playerStats.CurrentLvl++;
        _xpToNextLvl *= 2.5f;
        _currentXp = 0;
        _currentXp += xpDifference;
        RenewXpImage(_currentXp);
        LvlText.text = _currentLvl.ToString();
        foreach (NPCDialogueZone npc in nPCs)
        {
            npc.IsHaveQuest(_currentLvl);
        }

        eventBus.OnLvlUp?.Invoke();
    }

    private void RenewXpImage(float xp)
    {
        xpSlider.DOFillAmount(xp/_xpToNextLvl, 1.5f);
        XpText.text = $"{_currentXp}/{_xpToNextLvl}";
    }

}
