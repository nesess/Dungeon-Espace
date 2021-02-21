using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool damageCooldown = false;
    [SerializeField]
    private bool isGiant = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();


        if (hit != null)
        {
            if (damageCooldown == false)
            {
                if(isGiant)
                {
                    hit.Damage(2);
                }
                else
                {
                    hit.Damage(1);
                }
                damageCooldown = true;
                StartCoroutine(waitForNextAttack());
            }
        }

        IEnumerator waitForNextAttack()
        {
            yield return new WaitForSeconds(0.2f);
            damageCooldown = false;
        }
    }


}
