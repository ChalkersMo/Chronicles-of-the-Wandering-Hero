using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class PlayerLvl : MonoBehaviour
{
    [SerializeField] private Image xpSlider;
    [SerializeField] private TextMeshProUGUI LvlText;
    [SerializeField] private TextMeshProUGUI XpText;

    private PlayerStats playerStats;
    private NPCDialogueZone[] nPCs;

    private int _currentLvl = 1;
    private int _currentXp;
    private int _xpToNextLvl = 250;

    private void Start()
    {
        nPCs = FindObjectsOfType<NPCDialogueZone>();
        playerStats = GetComponent<PlayerStats>();
        playerStats.CurrentLvl = _currentLvl;
        LvlText.text = _currentLvl.ToString();
        RenewXpImage(_currentXp);
    }
    public void AddXp(int amount)
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
        _xpToNextLvl *= (int)2.5f;
        _currentXp = 0;
        RenewXpImage(_currentXp);
        LvlText.text = _currentLvl.ToString();
        foreach (NPCDialogueZone npc in nPCs)
        {
            npc.IsHaveQuest(_currentLvl);
        }
    }

    private void LvlUpWithDifferenceXp()
    {
        int xpDifference = _xpToNextLvl - _currentXp;
        _currentLvl++;
        playerStats.CurrentLvl++;
        _xpToNextLvl *= (int)2.5f;
        _currentXp = 0;
        _currentXp += xpDifference;
        RenewXpImage(_currentXp);
        LvlText.text = _currentLvl.ToString();
        foreach (NPCDialogueZone npc in nPCs)
        {
            npc.IsHaveQuest(_currentLvl);
        }
    }

    private void RenewXpImage(int xp)
    {
        xpSlider.DOFillAmount(xp/_xpToNextLvl, 1.5f);
        XpText.text = $"{_currentXp}/{_xpToNextLvl}";
    }

}
