using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouvement : MonoBehaviour
{
    [SerializeField] private InputActionAsset Actions;
    private InputAction moveAction, jumpAction;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 8f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private SpriteRenderer monSprite;
    private Animator anim;

    private Collider2D[] colls;
    private CapsuleCollider2D monColl;
    private bool grounded;

    private void OnEnable() {
        Actions.Enable();
        moveAction = Actions.FindAction("Move");
        moveAction.started += moveCheck;
        moveAction.canceled += moveCheck;

        jumpAction = Actions.FindAction("Jump");
        jumpAction.started += jumpCheck;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        monColl = GetComponent<CapsuleCollider2D>();
        monSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        groundCheck();
        animCheck();

        rb.linearVelocityX = speed * direction.x;
    }

    void moveCheck(InputAction.CallbackContext phase) {
        direction = phase.ReadValue<Vector2>();

        if(direction.x > 0) {
            monSprite.flipX = false;
        }
        if (direction.x < 0) {
            monSprite.flipX = true;
        }

        if (phase.canceled) {
            direction = Vector2.zero;
        }
        
    }

    void groundCheck() {
        grounded = false;
        colls = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, monColl.size.y / 2 - monColl.size.x * 0.35f, 0) + (Vector3)monColl.offset, monColl.size.x * 0.45f);
        foreach (Collider2D coll in colls) { 
            if(coll != monColl && !coll.isTrigger) {
                grounded = true; 
                break;
            }
        }
    }
    

    void jumpCheck(InputAction.CallbackContext phase) {
        if(grounded) {
            rb.linearVelocityY = jump;
        }        
    }

    void animCheck() {
        anim.SetFloat("velocityX", Mathf.Abs(rb.linearVelocityX));
        anim.SetFloat("velocityY", rb.linearVelocityY);
        anim.SetBool("grounded", grounded);
    }

    private void OnDrawGizmosSelected() {
        if ((monColl == null)) {
            monColl = GetComponent<CapsuleCollider2D>();
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, monColl.size.y/2 - monColl.size.x * 0.35f, 0) + (Vector3)monColl.offset, monColl.size.x * 0.45f);
    }
}