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

    [SerializeField] private Image DeathPanel;

    [SerializeField] private TextMeshProUGUI _healthTxt;

    private GlobalVolumeController volumeController;
    private AudioController audioController;
    private PlayerAudio playerAudio;
    private PlayerAnimController playerAnimController;
    private PlayerController playerController;

    private readonly float _fillSpeed = 1;
    private readonly float _fillDamageSpeed = 0.6f;

    private bool _isAlive = true;

    private void Start()
    {
        MaxHealth = PlayerStats.instance.MaxHealth;
        CurrentHealth = MaxHealth;
        _healthTxt.text = CurrentHealth.ToString();
        EventBus.Instance.OnLvlUp += HealOnLvlUp;
        volumeController = FindObjectOfType<GlobalVolumeController>();
        audioController = FindObjectOfType<AudioController>();
        playerAudio = GetComponent<PlayerAudio>();
        playerAnimController = GetComponent<PlayerAnimController>();
        playerController = GetComponent<PlayerController>();

        DeathPanel.DOFade(0, 3);
    }
    public void TakeDamage(float amount)
    {
        if (_isAlive)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            _healthTxt.text = CurrentHealth.ToString();
            volumeController.ChangeVignette(_takingDamageVignetteColor, 0.35f, 0.7f, 0.5f);
            StartCoroutine(OffVignette(0.5f));
            StartCoroutine(UpdateHealthBar());

            if (CurrentHealth <= 0)
            {
                _isAlive = false;
                StartCoroutine(DeathRoutine());
            }
        }       
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


    private void Recovering()
    {
        _isAlive = true;
        Heal(MaxHealth / 3);
        volumeController.ChangeVignette(Color.black, 0, 1, 3);
        DeathPanel.DOFade(0, 3);
        playerAnimController.RecoveringAnim();
    }

    private IEnumerator OffVignette(float duration)
    {
        yield return new WaitForSeconds(duration + 0.5f);
        volumeController.ChangeVignette(_lvlUpVignetteColor, 0f, 1, duration);
    }

    
    private IEnumerator UpdateHealthBar()
    {
        float targetFillAmount = CurrentHealth / MaxHealth;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _healthBarFill.DOColor(_colourgradient.Evaluate(targetFillAmount), _fillSpeed);
        yield return new WaitForSeconds(_fillSpeed);
        _healthBarDamageFill.DOFillAmount(targetFillAmount, _fillDamageSpeed);
        StopCoroutine(UpdateHealthBar());
    }

    private IEnumerator DeathRoutine()
    {
        playerAnimController.DeathAnim();
        playerAudio.RenewSource();
        playerAudio.PlayDeathSound();
        audioController.ChangeTheme(null, 3, false, 0);
        volumeController.ChangeVignette(Color.black, 1f, 0, 4);
        DeathPanel.DOFade(1, 4);
        yield return new WaitForSeconds(6);
        transform.position = playerController.CheckPointPosition;
        yield return new WaitForSeconds(1);
        Recovering();
    }
}
