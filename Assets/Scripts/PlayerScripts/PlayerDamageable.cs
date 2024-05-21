using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamageable : MonoBehaviour, IDamageable, IHealable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    [SerializeField] private Gradient _colourgradient;

    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Image _healthBarDamageFill;

    [SerializeField] private TextMeshProUGUI _healthTxt;

    private float _fillSpeed = 1;
    private float _fillDamageSpeed = 0.6f;

    private void Start()
    {
        MaxHealth = PlayerStats.instance.MaxHealth;
        CurrentHealth = MaxHealth;
        _healthTxt.text = CurrentHealth.ToString();
        EventBus.Instance.OnLvlUp += HealOnLvlUp;
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
    private void HealOnLvlUp()
    {
        MaxHealth += 15;
        Heal(10000);
    }
    IEnumerator UpdateHealthBar()
    {
        float targetFillAmount = CurrentHealth / MaxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _healthBarFill.DOColor(_colourgradient.Evaluate(targetFillAmount), _fillSpeed);
        yield return new WaitForSeconds(_fillSpeed);
        _healthBarDamageFill.DOFillAmount(targetFillAmount, _fillDamageSpeed);
        StopCoroutine(UpdateHealthBar());
    }

}
