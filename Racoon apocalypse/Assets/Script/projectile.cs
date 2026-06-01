using UnityEngine;

public class projectile : MonoBehaviour {
    public int degats = 1;          
    public float lifeTime = 5.0f;   

    private void Start() {
        Destroy(gameObject, lifeTime);
    } 
    
    void OnTriggerEnter2D(Collider2D truc) {
        Ennemi ennemi = truc.GetComponent<Ennemi>();
        
        if (ennemi != null)
        {
            ennemi.TakeDamage(degats); // Utilise ta variable degats, c'est plus propre !
            Destroy(gameObject);       // Pense à détruire le projectile quand il touche un ennemi
        }
        else if (truc.CompareTag("cage")) {
            // On essaie de récupérer le script de la cage
            DestructibleCage cage = truc.GetComponent<DestructibleCage>();
            
            if (cage != null) {
                // On laisse la cage gérer sa propre libération (score + destruction)
                cage.LibererAnimal();
            } else {
                // Sécurité : si le script n'est pas trouvé mais que le tag y est, on détruit quand même
                Destroy(truc.gameObject);
            }
            
            Destroy(gameObject);
        }
        else if (!truc.isTrigger && !truc.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}