using System;
using TMPro;
using UnityEngine;

public class PlayerSwordController : MonoBehaviour
{
    [SerializeField] SwordItem swordItem;
    public bool IsAttacking;

    [SerializeField] GameObject DamagePopUpPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && IsAttacking != false)
        {
            CreatePopUp(transform.position, $"{Math.Round(swordItem.Damage * PlayerStats.instance.MultiplySwordDamage), 1}", Color.gray);
        }     
    }
    public void CreatePopUp(Vector3 position, string text, Color color)
    {
        var popup = Instantiate(DamagePopUpPrefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        Destroy(popup, 1f);
    }
}
