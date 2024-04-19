using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    private GameObject _healthBar;

    private Image _healthBarFill;
    private Image _healthBarDamageFill;

    private TextMeshProUGUI _healthBarText;

    [SerializeField] Enemy enemyItem;

    [SerializeField] UnityEvent DieEvent;

    private Collider _collider;

    private float _fillSpeed = 1;
    private float _fillDamageSpeed = 0.4f;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private void Start()
    {
        _collider = GetComponent<Collider>();
        float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
        Vector3 healthBarPos = new Vector3(transform.position.x, colliderTop + 0.5f, transform.position.z);
        _healthBar = Instantiate(enemyItem.HealthBar, healthBarPos, Quaternion.identity, transform);
        _healthBarText = _healthBar.GetComponentInChildren<TextMeshProUGUI>();
        _healthBarFill = _healthBar.transform.GetChild(0).GetChild(2).GetComponentInChildren<Image>();
        _healthBarDamageFill = _healthBar.transform.GetChild(0).GetChild(1).GetComponentInChildren<Image>();

        MaxHealth = enemyItem.MaxHp;
        CurrentHealth = enemyItem.MaxHp;
        _healthBarText.text = CurrentHealth.ToString();
        StartCoroutine(IUpdateHelthBar());
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = (float)Math.Round(Mathf.Clamp(CurrentHealth, 0, MaxHealth), 2);
        _healthBarText.text  = CurrentHealth.ToString();
        StartCoroutine(IUpdateHelthBar());
        if (CurrentHealth <= 0)
        {
            StopCoroutine(IUpdateHelthBar());
            Die();
        }
    }
    IEnumerator IUpdateHelthBar()
    {
        float targetFillAmount = CurrentHealth / MaxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _healthBarFill.DOColor(enemyItem.HealthBarColorGradient.Evaluate(targetFillAmount), _fillSpeed);
        yield return new WaitForSeconds(_fillSpeed);
        _healthBarDamageFill.DOFillAmount(targetFillAmount, _fillDamageSpeed);
        StopCoroutine(IUpdateHelthBar());
    }
    
    void Die()
    {
        if(TryGetComponent(out QuestProgresser questProgresser))
            questProgresser.ProgressQuest();

        DieEvent?.Invoke();       
    }
}
