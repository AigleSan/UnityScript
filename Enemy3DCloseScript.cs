using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseScript : MonoBehaviour
{
    public float sightRange = 20;
    public float movementSpeed = 10;
    public Transform target;
    public float attackRange = 5;
    public int enemyDamage = 5;
    public int health = 100;
    private float cooldown = 0f;
    public float startCooldown = 3f;
    //public float stopDistance = 3;

    private PlayerAttack Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerAttack>();                 //on fait réference au script PlayerAttack pour pouvoir accéder les variables qu'elle contient
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < sightRange)
               {
                   //supposons que ce soit le script d'un ennemi au corps à corps
                   transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
      
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {

            if (cooldown <= 0)
            {
                Attack();
                cooldown = startCooldown;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }

        if (health <= 0)
        {

            Destroy(gameObject);
        }


    }



     void Attack()
    {        
        Debug.Log("Il t'as frappé, la honte");
        //Player.GetComponent<PlayerScript>().TakeDamage(enemyDamage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 1);
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, attackRange);


    }
}
