using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth = 0;
    private bool canTakeDamage = true;
    public float invincibilityFramesLength = 0.0f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void TakeDamage(int damageAmt)
    {
        if (canTakeDamage)
        {
            currentHealth -= damageAmt;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            if(currentHealth <= 0) 
            {
                HealthHitsZero();
            }
        }

    }

    void HealthHitsZero()
    {
        BroadcastMessage("Death");
    }
    IEnumerator Invincibility()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityFramesLength);
        canTakeDamage = true;
        yield return null;
    }
}
