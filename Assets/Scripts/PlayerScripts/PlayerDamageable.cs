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

    [SerializeField] private Color _takingDamageVignetteColor;
    [SerializeField] private Color _healVignetteColor;
    [SerializeField] private Color _lvlUpVignetteColor;

    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Image _healthBarDamageFill;

    [SerializeField] private TextMeshProUGUI _healthTxt;

    private GlobalVolumeController volumeController;

    private float _fillSpeed = 1;
    private float _fillDamageSpeed = 0.6f;

    private void Start()
    {
        MaxHealth = PlayerStats.instance.MaxHealth;
        CurrentHealth = MaxHealth;
        _healthTxt.text = CurrentHealth.ToString();
        EventBus.Instance.OnLvlUp += HealOnLvlUp;
        volumeController = FindObjectOfType<GlobalVolumeController>();
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        _healthTxt.text = CurrentHealth.ToString();
        volumeController.ChangeVignette(_takingDamageVignetteColor, 0.35f, 0.7f, 0.5f);
        StartCoroutine(OffVignette(0.5f));
        StartCoroutine(UpdateHealthBar());
    }
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        _healthTxt.text = CurrentHealth.ToString();
        volumeController.ChangeVignette(_healVignetteColor, 0.35f, 0.9f, 1);
        StartCoroutine(OffVignette(1));
        StartCoroutine(UpdateHealthBar());
    }
    private void HealOnLvlUp()
    {
        MaxHealth += 15;
        Heal(10000);
        volumeController.ChangeVignette(_lvlUpVignetteColor, 0.4f, 1, 2);
        StartCoroutine(OffVignette(2));
    }

    private IEnumerator OffVignette(float duration)
    {
        yield return new WaitForSeconds(duration + 0.5f);
        volumeController.ChangeVignette(_lvlUpVignetteColor, 0f, 1, duration);
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
