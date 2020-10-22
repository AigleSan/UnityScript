using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;

    public static Inventory instance;           //varaible statique qui nous permet d'acceder au scripit de n'importe ou
    private void Awake()            
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount = coinsCount + count;
        coinsCountText.text = coinsCount.ToString();            //pour pouvoir convertir la valeur numérique du nombre de piece en string .ToString()
    }
}
