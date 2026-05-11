using UnityEngine;

public class AnimalFollower : MonoBehaviour
{
    public float speed = 3f; // plus rapide pour tester
    private bool follow = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player introuvable ! Assurez-vous que le joueur a le tag 'Player'");
        }
    }

    public void Follow()
    {
        follow = true;
        Debug.Log("Animal commence à suivre !");
    }

    void Update()
    {
        if (!follow || player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Debug.DrawLine(transform.position, player.position, Color.green);
    }
}
