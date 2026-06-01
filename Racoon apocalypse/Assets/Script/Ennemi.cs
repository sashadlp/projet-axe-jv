using UnityEngine;

public class Ennemi : MonoBehaviour
{
   
    public int vie = 3;
    private bool isDead = false;

    
    public float speed = 2f;                                           
    [SerializeField, Range(0.1f, 50f)] private float limiteDroite = 1f;
    [SerializeField, Range(0.1f, 50f)] private float limiteGauche = 1f;
    
    private Vector3 limiteDroitePosition;                              
    private Vector3 limiteGauchePosition;                              
    private float direction = 1f;                                      

    
    private Rigidbody2D rb;                                             
    private SpriteRenderer skin;                                        
    private Animator anim;
    private Collider2D col;


    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        skin = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        CalculerLimites();
    }

    void Update() 
    {
        if (isDead) return;

        MettreAJourPatrouille();
        RetournerSprite();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }
    }

    private void MettreAJourPatrouille()
    {
        if (transform.position.x > limiteDroitePosition.x) 
        {
            direction = -1f;
        }
        else if (transform.position.x < limiteGauchePosition.x) 
        {
            direction = 1f;
        }
    }

    private void RetournerSprite()
    {
        if (skin != null)
        {
            skin.flipX = (direction == -1f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        vie -= damage;
        
        if (anim != null) anim.SetTrigger("ouch");

        if (vie <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (anim != null) anim.SetTrigger("dead");

        if (col != null) col.enabled = false;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        Destroy(gameObject, 1.5f);
    }

   
    private void CalculerLimites()
    {
        limiteDroitePosition = transform.position + new Vector3(limiteDroite, 0, 0);
        limiteGauchePosition = transform.position - new Vector3(limiteGauche, 0, 0);
    }

    void OnDrawGizmos() 
    {
        if (!Application.IsPlaying(gameObject)) 
        {
            CalculerLimites();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(limiteDroitePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawCube(limiteGauchePosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawLine(limiteDroitePosition, limiteGauchePosition);
    }
}