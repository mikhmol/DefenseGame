using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    static public Action<bool> attack;
    
    public void AttackButton()
    {
        attack?.Invoke(true);
    }
}
