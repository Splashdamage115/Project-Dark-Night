using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float regenTime = 1.5f;

    public int maxHealth = 2;
    private int currentHealth = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    IEnumerator regenHealth()
    {
        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(regenTime);
            currentHealth++;
        }
        yield return null;
    }

    void applyDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Expire();
            return;
        }
        StartCoroutine(regenHealth());
    }

    void Expire()
    {
        // kill the player here!
    }
}
