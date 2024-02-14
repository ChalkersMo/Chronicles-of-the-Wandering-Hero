using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float speed;
    public float runningSpeed;
    public float MultiplySwordDamage;
    public float MaxHealth;
    void Awake ()
    {
        instance = this;
    }

}
