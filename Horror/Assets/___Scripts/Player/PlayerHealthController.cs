using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthController : MonoBehaviour
{
    public Material material;
    private bool regenActive = false;
    public float regenTime = 1.5f;

    public int maxHealth = 2;
    [SerializeField]
    private int currentHealth = 0;

    private float moveToAmount = 0.0f;
    private float visibility = 0f;

    public float screenFadeTime = 0.2f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    IEnumerator regenHealth()
    {
        if (regenActive) { yield return null; }
        else { 
            regenActive = true;
            while (currentHealth < maxHealth)
            {
                yield return new WaitForSeconds(regenTime);
                currentHealth++;
                moveToAmount = Mathf.Abs(currentHealth / maxHealth - 1);
                StartCoroutine(RegenScreen());
            }
            regenActive = false;
        }
        yield return null;
    }

    IEnumerator RegenScreen()
    {
        while (!Mathf.Approximately(visibility, moveToAmount))
        {
            visibility = Mathf.Lerp(visibility, moveToAmount, screenFadeTime * Time.deltaTime);
            visibility = Mathf.Clamp(visibility, 0, 1);
            material.SetFloat("_Visibility", visibility);
            yield return null;

            if (Mathf.Approximately(material.GetFloat("_Visibility"), 0f))
                break;
        }
        material.SetFloat("_Visibility", 0);
        yield return null;
    }

    void applyDamage(int damage)
    {
        currentHealth -= damage;
        material.SetFloat("_Visibility", Mathf.Abs(currentHealth / maxHealth - 1));
        visibility = Mathf.Abs(currentHealth / maxHealth - 1);

        if (currentHealth <= 0)
        {
            Expire();
            return;
        }
        if(!regenActive)
            StartCoroutine(regenHealth());
    }

    void Expire()
    {
        // kill the player here!
    }
}
