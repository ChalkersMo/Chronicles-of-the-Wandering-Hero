using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float speed;
    public float runningSpeed;
    void Start()
    {
        instance = this;
    }

}
