using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackDIST : MonoBehaviour {

    // SCRIPT A METTRE SUR LE JOUEUR, il marche en combinaison avec un projectile (qui sera un object avec un visuel, un trigger et un rigidbody. Exemple : un boulet de canon)
    // Ce script permet de tirer tout droit devant soit

    public GameObject projectil;                // Le prefab du projectile que l'on tir, on doit glisser dans cette case un prefab avec un trigger ET un rigidbody2D
    private GameObject projectilSave;           // Une sauvegarde temporaire du projectile tiré pour lui apporté quelques modification quand on l'invoque

    public float speedProjectil = 1f;           // La vitesse de déplacement de notre projectile (valeur de base = 1)

    public float reloadTime = 0.5f;             // Le temps de chargement entre 2 tirs (valeur de base = 0.5)
    private bool reloading;                     // Booléen qui devient vrai le temps qu'on recharge

    private SpriteRenderer skin;                // Le sprite du joueur, on va s'en servir pour savoir si il regarde à gauche ou a droite

    private Animator anim;

    // Dans le start on récupère le sprite renderer et l'animator
    void Start() {
        skin = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Dans l'update On va simplement vérifier si le jouer appuis sur le bouton de tir (ici le clic droit de la souris)
    void Update() {
        
        // Si le joueur appuis sur Clic-droit (Fire2), ET qu'il n'est pas entrain de recharger (reloading = false)
        if (Input.GetButtonDown("Fire2") && !reloading) {
            
            anim.SetTrigger("attackDIST"); // On lance l'animation d'attaque, si vous n'avez pas d'animation d'attaque sur votre personnage vous pouvez supprimer cette ligne

            if (!skin.flipX) {                                                                          // 1. Si le sprite n'es pas retourner (donc regarde à droite)
                projectilSave = Instantiate(projectil, transform.position, Quaternion.identity);        // 2. On faire apparaitre un clone de projectile sur notre position
                projectilSave.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(speedProjectil, 0);    // 3. On va chercher le rigidbody du projectile, et le propulse vers la DROITE
            }

            if (skin.flipX) {                                                                           // 1. Si le sprite est retourner (donc regarde à gauche) 
                projectilSave = Instantiate(projectil, transform.position, Quaternion.identity);        // 2. On faire apparaitre un clone de projectile sur notre position
                projectilSave.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(- speedProjectil, 0);  // 3. On va chercher le rigidbody du projectile, et le propulse vers la GAUCHE
                projectilSave.GetComponent<SpriteRenderer>().flipX = true;                              // 4. Vu qu'on envoie le projectile a gauche, on inverse son sprite pourqu'il regarde dans la bonne direction
            }
            reloading = true;               // Vu qu'on vient de tirer on passe reloading en VRAI 
            StartCoroutine(waitShoot());    // Enfin on lance une coroutine waitShoot qui va attendre un certain temps (reloadTime), pour remettre reloading en VRAI
        }
    }

    // Voici la coroutine waitShoot
    IEnumerator waitShoot() {
        yield return new WaitForSeconds(reloadTime); // La on dit au script de patienter pendant un certain temps (reloadTime)
        reloading = false;                           // On a fini d'attendre donc on repasse reloading en vrai, donc on va pouvoir tirer à nouveau
    }
}
