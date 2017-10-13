using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 5f;
    [SerializeField]
    private float health;
    private float timeBetweenDamage = 0.8f;
    private float damageTimer;
    [SerializeField]
    private Rooms room;
    public bool isActive = false;
    
    private void Start()
    {
        room = GetComponentInParent<Rooms>();
        health = maxHealth;
        damageTimer = timeBetweenDamage;
    }

    private void Update()
    {
        if(isActive && room.playerInPosition)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0)
            {
                TakeDamage(1f);
                PlayerHealth.changeHealth(-1);
                damageTimer = timeBetweenDamage;
            }
        }
        else
        {
            damageTimer = timeBetweenDamage;
        }
    }

    public void TakeDamage(float damage)
    {
        //PlayerHealth.changeHealth(-1);
        health -= damage;
        if(health <= 0)
        {
            DestroyFire();
        }
    }

    void DestroyFire()
    {
        isActive = false;
        health = maxHealth;
        gameObject.SetActive(false);
    }
}
