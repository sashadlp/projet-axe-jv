using UnityEngine;

public class projectile : MonoBehaviour {

    // Script a mettre sur votre projectile que vous allez tirer depuis le script attackDIST
    // IL doit y avoir un TRIGGER sur l'objet et un rigidbody

    public int degats = 1;          // Les dégats du projectile
    public float lifeTime = 5.0f;   // Le temps maximal que vivra le projectile (pour être sur qu'il se détruise au bout d'un moment)

    private void Start() {
        Destroy(gameObject, lifeTime);
    }

    // La fonction OnTriggerEnter s'enclenche quand votre Trigger touche un autre collider/trigger
    void OnTriggerEnter2D(Collider2D truc) {
    if (truc.tag == "Ennemi") {
        truc.SendMessage("takeDamage", degats);
        Destroy(gameObject);
    }
    else if (truc.tag == "cage") {
        // Détruire la cage
        Destroy(truc.gameObject);
        Destroy(gameObject); // On détruit aussi le projectile
    }
    else if (!truc.isTrigger && truc.tag != "Player") {
        Destroy(gameObject);
    }
}

}
