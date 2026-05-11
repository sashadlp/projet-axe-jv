using UnityEngine;

public class EnnemiLife : MonoBehaviour
{
    public int vie = 3;

    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void takeDamage(int damage)
    {
        if (isDead) return;

        vie -= damage;
        anim.SetTrigger("ouch");

        if (vie <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("dead");

        // Désactive le script de déplacement si il existe
        var patrol = GetComponent<ennemiPatrol>();
        if (patrol != null) patrol.enabled = false;

        // Désactive les collisions
        if (col != null) col.enabled = false;

        // Coupe la physique
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        // Option : détruire après l'animation
        Destroy(gameObject, 1.5f);
    }
}
