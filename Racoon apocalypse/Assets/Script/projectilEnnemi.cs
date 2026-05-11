using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilEnnemi : MonoBehaviour
{
    public int degats = 1;
    public float lifeTime = 5.0f;

    private void Start() {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D truc) {
        if (truc.tag == "Player") {
            truc.SendMessage("takeDamage", degats);
            Destroy(gameObject);
        } else if (!truc.isTrigger && truc.tag != "Ennemi") {
            Destroy(gameObject);
        }
    }
}
