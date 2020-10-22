using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;         //valeur maximale des PV
    public int currentHealth;
    public SpriteRenderer graphics;

    public float invincibiltyFlashDelay = 0.2f;
    public float invincibiltyTime = 3f;
    public bool isInvincible = false;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);          ///la barre de PV soit visuelement au max
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))                                //si on appuie sur la touche H
        {

            TakeDamage(20);                                             //on prend 20 points de degats
        }
    }

    public void TakeDamage(int damage)                                  //nous avons rendu cette fonction publique qui permet de prendre des degats pour pouvoir y faire appel depuis d'autres scripts

    {

        if (!isInvincible)
        {
            currentHealth = currentHealth - damage;
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibiltyFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibiltyFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibiltyTime);
        isInvincible = false;
    }
}
