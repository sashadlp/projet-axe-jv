using System.Collections;
using System.Collections.Generic;
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
    private bool reloading;

    private Vector3 direction;
    private float angleProjectil;

    private SpriteRenderer skin;

    private Animator anim;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        skin = GetComponent<SpriteRenderer>();
        positionWeapon = weapon.localPosition;
        anim = GetComponent<Animator>();
    }

    void Update() {
        lookCheck();
        direction = player.position - weapon.position;
        direction.Normalize();
        angleProjectil = Vector3.SignedAngle(transform.right, direction, Vector3.forward);

        if (Vector2.Distance(transform.position, player.position) < distanceAggro && !reloading) {
            if (anim != null) {
                //anim.SetTrigger("attackDIST");
            }            

            reloading = true;
            projectilSave = Instantiate(projectil, weapon.position, Quaternion.Euler(0, 0, angleProjectil));
            projectilSave.GetComponent<Rigidbody2D>().linearVelocity = direction * speedProjectil;
            projectilSave.GetComponent<projectilEnnemi>().degats = degats;
            StartCoroutine(waitShoot());
        }
    }

    void lookCheck() {
        if (transform.position.x < player.position.x) {
            skin.flipX = false;
            weapon.localPosition = positionWeapon;
        }
        if (transform.position.x > player.position.x) {
            skin.flipX = true;
            weapon.localPosition = new Vector3(-positionWeapon.x, positionWeapon.y, 0);
        }
    }

    IEnumerator waitShoot() {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
