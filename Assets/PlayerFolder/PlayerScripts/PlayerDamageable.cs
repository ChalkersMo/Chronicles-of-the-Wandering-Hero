using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamageable : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    float _currentHealth;
    [SerializeField] Image _healthBarFill;
    [SerializeField] Image _healthBarDamageFill;
    [SerializeField] TextMeshProUGUI _healthTxt;
    float _fillSpeed = 1;
    float _fillDamageSpeed = 0.6f;
    [SerializeField] Gradient _colourgradient;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthTxt.text = _currentHealth.ToString();

    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthTxt.text += _currentHealth.ToString();
        StartCoroutine(UpdateHealthBar());
    }
    public void Heal(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthTxt.text += _currentHealth.ToString();
        StartCoroutine(UpdateHealthBar());
    }

    IEnumerator UpdateHealthBar()
    {
        float targetFillAmount = _currentHealth / _maxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        yield return new WaitForSeconds(_fillSpeed);
        _healthBarDamageFill.DOFillAmount(targetFillAmount, _fillDamageSpeed);
        StopCoroutine(UpdateHealthBar());
    }

}
