using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI animalsSavedText;
    
    public void Show(int scoreFinal)
    {
        gameObject.SetActive(true);
        
        if (animalsSavedText != null)
        {
            animalsSavedText.text = "Animaux sauvés : " + scoreFinal.ToString();
        }
    }
}