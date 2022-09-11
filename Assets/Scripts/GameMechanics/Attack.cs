using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    static public Action<bool> action;
    
    public void AttackButton()
    {
        action?.Invoke(true);
    }
}
