using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)                    //fonction pour donner a la barre de vie la valeur de base des PV
    {
        slider.maxValue = health;                          //donner initialement la valeur maximale de PV
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)                       //foncrion pour donner a la barre de vie une quantite de PV
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
