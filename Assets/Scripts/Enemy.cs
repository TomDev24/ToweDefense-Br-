using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    public float startSpeed = 10f;

    [SerializeField]
    private GameObject deathEffect;

    [HideInInspector]
    public float speed; // we want speed to be accsecible in the EnemyMovement but not in the Inscpector

    private float health; // before was int but we changed in EP16
    public float startHealth = 100;
    public int worth = 25;

    private bool isDead = false;


    private void Start()
    {
        health = startHealth;
        speed = startSpeed;    
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
            Die();
    }

    private void Die()
    {
        isDead = true;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        PlayerStats.Money += worth;

        Destroy(gameObject);
    }

    internal void Slow(float slowFactor)
    {
        speed = startSpeed * (1 - slowFactor); // created start speed because of every frame update ep16
    }
}
