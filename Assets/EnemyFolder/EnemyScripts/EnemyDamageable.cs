using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyDamageable : MonoBehaviour
{
    GameObject _healthBar;
    Image _healthBarFill;
    Image _healthBarDamageFill;
    TextMeshProUGUI _healthBarText;
    [SerializeField] Enemy enemyItem;
    [SerializeField] UnityEvent DieEvent;
    Collider _collider;
    float _fillSpeed = 1;
    float _fillDamageSpeed = 0.4f;
    float _currentHealth;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        float colliderTop = _collider.bounds.center.y + _collider.bounds.extents.y;
        Vector3 healthBarPos = new Vector3(transform.position.x, colliderTop + 0.5f, transform.position.z);
        _healthBar = Instantiate(enemyItem.HealthBar, healthBarPos, Quaternion.identity, transform);
        _healthBarText = _healthBar.GetComponentInChildren<TextMeshProUGUI>();
        _healthBarFill = _healthBar.transform.GetChild(0).GetChild(2).GetComponentInChildren<Image>();
        _healthBarDamageFill = _healthBar.transform.GetChild(0).GetChild(1).GetComponentInChildren<Image>();

        _currentHealth = enemyItem.MaxHp;
        _healthBarText.text = _currentHealth.ToString();
        StartCoroutine(IUpdateHelthBar());
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = (float)Math.Round(Mathf.Clamp(_currentHealth, 0, enemyItem.MaxHp), 2);
        _healthBarText.text  = _currentHealth.ToString();
        StartCoroutine(IUpdateHelthBar());
        if (_currentHealth <= 0)
            Die();
    }
    IEnumerator IUpdateHelthBar()
    {
        float targetFillAmount = _currentHealth / enemyItem.MaxHp;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _healthBarFill.DOColor(enemyItem.HealthBarColorGradient.Evaluate(targetFillAmount), _fillSpeed);
        yield return new WaitForSeconds(_fillSpeed);
        _healthBarDamageFill.DOFillAmount(targetFillAmount, _fillDamageSpeed);
        StopCoroutine(IUpdateHelthBar());
    }
    
    void Die()
    {
        DieEvent?.Invoke();
    }
}
