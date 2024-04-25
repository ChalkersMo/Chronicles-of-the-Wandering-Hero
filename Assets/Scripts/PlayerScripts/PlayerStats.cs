using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float speed;
    public float runningSpeed;
    public float MultiplySwordDamage;
    public float MaxHealth;
    public float HealingMultiply;
    public float XP;

    public int CurrentLvl;

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

}
