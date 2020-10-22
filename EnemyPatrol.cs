using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;         //variable qui servira a donner la vitesse a l'ennemi

    public Transform[] waypoints;       //array qui permettra de mettre les waypoints de lennemi

    public int damageOnCollision = 20;
    public SpriteRenderer graphics;

    private Transform target;           // on defini la cible de l'ennemi qui variera entre le waypoint A et le waypoint B
    private int destPoint = 0;          //désigne la meme chose que target mais sous forme de int il sert d'index


    void Start()
    {
        target = waypoints[0];
       graphics.flipX = !graphics.flipX;                              //on flip l'ennemi une fois avant que la condition dans le voidUpdate le fasse selon le sens dans lequel va lennemi
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)           //on verifie si la distance entre l'ennemi et sa cible est inferieure  à 3
        {

            destPoint = (destPoint + 1) % waypoints.Length;         //l'operateur % permet de recuperer le reste d'une division
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;                             //selon la distance avec la target actuelle lennemi se tourne
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)          //Cette fonction permettra a lennemi d'infliger des degats a notre joueuer si il rentre en collision
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }


}
