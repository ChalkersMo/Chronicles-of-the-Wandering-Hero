using System;
using TMPro;
using UnityEngine;

public class PlayerSwordController : MonoBehaviour
{
    public SwordItem swordItem;
    public bool IsAttacking;

    [SerializeField] GameObject DamagePopUpPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && IsAttacking != false)
        {
            float damage = (float)Math.Round(swordItem.Damage * PlayerStats.instance.MultiplySwordDamage, 2);
            other.GetComponent<EnemyDamageable>().TakeDamage(damage);
            CreatePopUp(transform.position, $"{damage}", swordItem.DamagePopUpFaceColor, swordItem.DamagePopUpoutlineColor);
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
}
