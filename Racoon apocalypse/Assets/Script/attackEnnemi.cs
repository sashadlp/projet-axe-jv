using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEnnemi : MonoBehaviour
{
    public int degats = 2;
    public Vector2 attackPosition;
    private Vector2 attackPositionSave;
    public float attackRadius;
    public float reloadTime = 0.5f;
    private bool reloading;
    private Animator anim;
    private Collider2D[] target;
    private SpriteRenderer skin;
    private RaycastHit2D hit;
    private ennemiPatrol script;

    void Start() {
        anim = GetComponent<Animator>();
        skin = GetComponent<SpriteRenderer>();
        script = GetComponent<ennemiPatrol>();
        attackPositionSave = attackPosition;
    }

    void Update() {
        if (!skin.flipX) {
            hit = Physics2D.Raycast(transform.position, Vector2.right, attackPosition.x);
            Debug.DrawRay(transform.position, Vector2.right * attackPosition.x, Color.cyan);

            if (hit && hit.transform.tag == "Player" && !reloading) {
                attackPosition = (Vector2)transform.position + new Vector2(attackPositionSave.x, attackPositionSave.y);
                StartCoroutine(waitShoot());
            }
        }
        if (skin.flipX) {
            hit = Physics2D.Raycast(transform.position, Vector2.left, attackPosition.x);
            Debug.DrawRay(transform.position, Vector2.left * attackPosition.x, Color.cyan);

            if (hit && hit.transform.tag == "Player" && !reloading) {
                attackPosition = (Vector2)transform.position + new Vector2(-attackPositionSave.x, attackPositionSave.y);
                StartCoroutine(waitShoot());
            }
        }

    }

    IEnumerator waitShoot() {
        script.enabled = false;
        reloading = true;
        anim.SetTrigger("attackCAC");
        yield return new WaitForSeconds(0.5f);
        target = Physics2D.OverlapCircleAll(attackPosition, attackRadius);
        foreach (Collider2D truc in target) {
            if (truc.tag == "Player") {
                truc.SendMessage("takeDamage", degats);
            }
        }
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        script.enabled = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + attackPosition, attackRadius);
    }
}
