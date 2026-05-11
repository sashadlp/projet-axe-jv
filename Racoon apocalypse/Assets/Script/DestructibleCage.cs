using UnityEngine;

public class DestructibleCage : MonoBehaviour
{
    public int pv = 3;
    public AnimalFollower animalInside;

    public void TakeDamage(int dmg)
    {
        pv -= dmg;

        if (pv <= 0)
        {
            animalInside.Follow(); 
            Destroy(gameObject);
        }
    }

}
