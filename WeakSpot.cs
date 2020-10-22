using UnityEngine;

public class WeakSpot : MonoBehaviour
{

    public GameObject objectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)                 //cette methode est lue a chaque fois qu'un objet entre en contact avec la zone, l'argument collision fait reference a ce qui rentre dans la zone si l'objet qui rentre a un rigidbody
    {
        if (collision.CompareTag("Player"))                                 //si l'element qui est entre en collision avec la zon est le joueur
        {
            Destroy(objectToDestroy);                           //detruire le parent de cet objet et ses sous objets
        }
    }
}
