using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 25;
    private float cooldown;
    public float startCooldown;
    public LayerMask whatIsEnemies;

    public Transform attackPos; //??
    public float attackRange;

    public float movementSpeed;
    public float jumpHeight;
    private bool canJump;          //verifie si on a sauté
    private int numbJumps;          //on va tenter le double saut

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * movementSpeed * Time.deltaTime;
        var movementZ = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(0f, 0f, movementZ) * movementSpeed * Time.deltaTime;


        Vector3 direction = new Vector3(movementX, 0f, movementZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        if (canJump == true)                    //uniquement si on peut sauter donc si on est contact avec le sol c'est peut être pas idéal si on saute sur des platformes qui sont pas le sol à optimiser
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                canJump = false;                                //on ne peut plus sauter vu qu'on est en l'air voyons
            }

        }

        if (cooldown <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Attack();
                cooldown = startCooldown;
            }

        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")           //si on est en contact avec les elements qui ont pour tag Ground
        {
            canJump = true;                             //nous avons le droit de sauter
            Debug.Log("on ground");                     //une indication juste pour indiquer qu'on est sur le sol


        }

    }

    void Attack()
    {
        Collider[] enemiesToDamage = Physics.OverlapSphere(transform.position, attackRange, whatIsEnemies);

        foreach (Collider enemy in enemiesToDamage)
        {
            Debug.Log("GROS DEGATS");
            enemy.GetComponent<EnemyCloseScript>().TakeDamage(attackDamage);
        }
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
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}

