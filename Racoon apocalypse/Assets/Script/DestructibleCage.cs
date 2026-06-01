using UnityEngine;

public class DestructibleCage : MonoBehaviour
{

    public void LibererAnimal()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoint();
        }

     
        Destroy(gameObject);
    }

  
    private void OnMouseDown()
    {
        LibererAnimal();
    }
}