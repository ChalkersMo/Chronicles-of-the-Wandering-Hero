using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    static EventBus _instance;
    public static EventBus Instance => _instance;
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);     
    }
    public Action OnAttack;
    public Action OnHit;
    public Action OnTakeDamage;
    public Action OnHeal;
}
