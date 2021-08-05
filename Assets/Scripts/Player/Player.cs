using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Player : MonoBehaviour
{
    public abstract Action isTakeDamage { get; set; }
    public abstract Action isTakeCoins { get; set; }
    public abstract float healthPoints { get; set; }
    public abstract int coinCount { get; set; }
}
