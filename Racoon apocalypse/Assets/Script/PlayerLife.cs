using UnityEngine;

using UnityEngine.UI;

public class playerLife : MonoBehaviour
{
    [SerializeField] private int vie = 5;
    private int vieMax;
    private Slider barreDevie;
    private Vector3 posBase;

    private void Start() {
        vieMax = vie;
        barreDevie = GameObject.FindWithTag("LifeBar").GetComponent<Slider>();
        posBase = transform.position;
        updateBar();
    }

    public void takeDamage(int damage)
{
    vie -= damage;
    if (vie <= 0) { 
        transform.position = posBase;
        vie = vieMax;
    }
    updateBar();
}


    void updateBar() {
        barreDevie.value = (float)vie / (float)vieMax;
    }

    void OnTriggerEnter2D(Collider2D truc) {
        if(truc.tag == "Piege") {
            takeDamage(1);
            Destroy(truc.gameObject);
        }
        if (truc.tag == "Respawn")
        {
            posBase = transform.position;
        }
        if (truc.tag == "Kill")
        {
            takeDamage(vieMax);
        }
    }
}
