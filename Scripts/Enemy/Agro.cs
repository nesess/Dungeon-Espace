using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agro : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Enemy enemy = GetComponentInParent<Enemy>();
            enemy.isHit = true;
            enemy.anim.SetBool("InCombat", true);
        }
    }
}
