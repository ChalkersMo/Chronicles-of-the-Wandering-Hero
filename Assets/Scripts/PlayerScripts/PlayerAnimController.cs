using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimController : MonoBehaviour
{
    Animator animatorController;
    private void Start()
    {
        animatorController = GetComponentInChildren<Animator>();
    }

}
