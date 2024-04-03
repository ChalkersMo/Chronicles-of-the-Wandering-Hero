using System;
using TMPro;
using UnityEngine;

public class PlayerSwordController : MonoBehaviour
{
    public SwordItem swordItem;
    public bool IsAttacking;

    [SerializeField] GameObject DamagePopUpPrefab;
    EventBus eventBus;
    EnemyDamageable _enemyDamageable;
    private void OnEnable()
    {
        eventBus = EventBus.Instance;
        eventBus.OnHit += Hit;
    }
    private void OnDisable()
    {
        eventBus.OnHit -= Hit;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsAttacking)
        {
            if (other.TryGetComponent(out EnemyDamageable enemyDamageable))
            {
                _enemyDamageable = enemyDamageable;
                eventBus.OnHit?.Invoke();
            }
        }
    }     
    
    public void CreatePopUp(Vector3 position, string text, Color faceColor, Color outlineColor)
    {
        var popup = Instantiate(DamagePopUpPrefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = faceColor;
        temp.outlineColor = outlineColor;
       
        Destroy(popup, 1f);
    }
    void Hit()
    {
        if (_enemyDamageable != null)
        {
            float damage = (float)Math.Round(swordItem.Damage * PlayerStats.instance.MultiplySwordDamage, 2);
            _enemyDamageable.TakeDamage(damage);
            CreatePopUp(transform.position, $"{damage}", swordItem.DamagePopUpFaceColor, swordItem.DamagePopUpoutlineColor);
        }
    }

}
