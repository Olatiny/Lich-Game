using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField][Tooltip("the player's max health")]private int maxHealth;
    [SerializeField][Tooltip("the player's current health")]private int currentHealth;
    [SerializeField][Tooltip("the player's i frame duration. How long the player is invincible after being hurt")]private float iFrames;
    [SerializeField][Tooltip("the player's current iFrame timer. Shows how much time the player has left until they can take damage again")]private float iFrameTimer;
    [SerializeField][Tooltip("the damaging layer which will be used to hurt the player on collision")]private string DamageLayer = "Damage";
    [SerializeField][Tooltip("the healing layer which will be used to heal the player on collision")]private string HealLayer = "Heal";
    [SerializeField][Tooltip("the damage particle effect which should be played when the player is hit")]private GameObject hurtParticles;
    
    void Start(){
        currentHealth = maxHealth;
    }

    public int GetHealth(){
        return currentHealth;
    }

    public void ResetHealth(){
        currentHealth = maxHealth;
        iFrameTimer = 0;
    }

    public void SetHealth(int newHealth){
        currentHealth = newHealth;
    }

    public void RestoreHealth(int healthIncrease){
        GameManager.Instance.GainedHealth(currentHealth,healthIncrease);
        currentHealth = Mathf.Min(currentHealth + healthIncrease, maxHealth);
        Debug.Log("health gain");
    }

    public void Damage(int damageTaken){
        if(iFrameTimer > 0){
            return;
        }
        iFrameTimer = iFrames;
        GameManager.Instance.TookDamage(currentHealth,damageTaken);
        currentHealth -= damageTaken;
        if(currentHealth < 1){
            Die();
        }
    }

    public void Die(){
        GameManager.Instance.Die();
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.collider.gameObject.layer == LayerMask.NameToLayer(DamageLayer)){
            Damage(1);
        }
    }


    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("trigger");
        if(col.gameObject.layer == LayerMask.NameToLayer(DamageLayer)){
            Debug.Log("damage trigger");
            Damage(1);
            Destroy(col.gameObject);
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer(HealLayer)){
            Debug.Log("heal trigger");
            if(col.gameObject.GetComponent<HealItem>()){
                RestoreHealth(col.gameObject.GetComponent<HealItem>().GetHealth());
            }
            Destroy(col.gameObject);
        }
    }

    void FixedUpdate(){
        if(iFrameTimer >= 0){
            iFrameTimer -= Time.fixedDeltaTime;
        }
    }
}
