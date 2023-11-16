using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossCube : MonoBehaviour
{
    private int health = 2;

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
