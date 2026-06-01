using System.Collections;
using UnityEngine;

public class attackEnnemiDIST : MonoBehaviour
{
    public int degats = 1;
    public float distanceAggro = 10f;
    public Transform weapon;
    private Vector3 positionWeapon;
    public GameObject projectil;
    private GameObject projectilSave;
    private Transform player;

    public float speedProjectil = 1f;
    public float reloadTime = 0.5f;
    
    private float reloadCountdown = 0f;

    private Vector3 direction;
    private float angleProjectil;

    private Animator anim;

    void Start() {
        if (Gameloop.Instance != null && Gameloop.Instance.PlayerTransform != null) {
            player = Gameloop.Instance.PlayerTransform;
        } else {
            Debug.LogError("Attention : Impossible de récupérer le joueur via Gameloop.Instance !");
        }

        anim = GetComponent<Animator>();

        if (weapon != null) {
            positionWeapon = weapon.localPosition;
        } else {
            Debug.LogError($"Il manque le Transform 'Weapon' sur le script d'attaque de {gameObject.name} !");
        }
    }

    void Update() {
        if (player == null || weapon == null) return;

        lookCheck();

        direction = player.position - weapon.position;
        direction.Normalize();
        angleProjectil = Vector3.SignedAngle(transform.right, direction, Vector3.forward);

        float distanceAuJoueur = Vector2.Distance(transform.position, player.position);

        if (anim != null) {
            anim.SetBool("isAttacking", distanceAuJoueur < distanceAggro);
        }

      
        if (reloadCountdown > 0f) {
            reloadCountdown -= Time.deltaTime;
        }

        if (distanceAuJoueur < distanceAggro && reloadCountdown <= 0f) {
            
            reloadCountdown = reloadTime;
            
            projectilSave = Instantiate(projectil, weapon.position, Quaternion.Euler(0, 0, angleProjectil));
            
            if (projectilSave.GetComponent<Rigidbody2D>() != null) {
                projectilSave.GetComponent<Rigidbody2D>().linearVelocity = direction * speedProjectil;
            }
            
            if (projectilSave.GetComponent<projectilEnnemi>() != null) {
                projectilSave.GetComponent<projectilEnnemi>().degats = degats;
            }
        }
    }

    void lookCheck() {
        if (transform.position.x < player.position.x) {
            weapon.localPosition = positionWeapon;
        }
        else if (transform.position.x > player.position.x) {
            weapon.localPosition = new Vector3(-positionWeapon.x, positionWeapon.y, 0);
        }
    }
}