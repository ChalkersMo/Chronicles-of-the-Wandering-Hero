using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamageable : MonoBehaviour, IDamageable, IHealable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    [SerializeField] Image _healthBarFill;
    [SerializeField] Image _healthBarDamageFill;
    [SerializeField] TextMeshProUGUI _healthTxt;
    float _fillSpeed = 1;
    float _fillDamageSpeed = 0.6f;
    [SerializeField] Gradient _colourgradient;

    private void Start()
    {
        MaxHealth = PlayerStats.instance.MaxHealth;
        CurrentHealth = MaxHealth;
        _healthTxt.text = CurrentHealth.ToString();

    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        _healthTxt.text = CurrentHealth.ToString();
        StartCoroutine(UpdateHealthBar());
    }
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        _healthTxt.text = CurrentHealth.ToString();
        StartCoroutine(UpdateHealthBar());
    }

    IEnumerator UpdateHealthBar()
    {
        float targetFillAmount = CurrentHealth / MaxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        yield return new WaitForSeconds(_fillSpeed);
        _healthBarDamageFill.DOFillAmount(targetFillAmount, _fillDamageSpeed);
        StopCoroutine(UpdateHealthBar());
    }

}
