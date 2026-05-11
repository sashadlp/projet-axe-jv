using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float BackgroundScale = 1f;
    [SerializeField] private Sprite[] Backgrounds;
    private Transform[] bgPos;
    private Transform maCamera;
    private float camPosX;

    void Start() {
        maCamera = Camera.main.transform;
        camPosX = maCamera.position.x;
        bgPos = new Transform[Backgrounds.Length];

        for (int i = 0 ; i < Backgrounds.Length ; i++) {            
            bgPos[i] = new GameObject(Backgrounds[i].name).transform;
            bgPos[i].AddComponent<SpriteRenderer>().sprite = Backgrounds[i];

            Transform bgAnnexe = new GameObject(Backgrounds[i].name).transform;
            bgAnnexe.parent = bgPos[i];
            bgAnnexe.position = bgPos[i].position + Vector3.left * Backgrounds[i].bounds.size.x;
            bgAnnexe.AddComponent<SpriteRenderer>().sprite = Backgrounds[i];

            bgAnnexe = new GameObject(Backgrounds[i].name).transform;
            bgAnnexe.parent = bgPos[i];
            bgAnnexe.position = bgPos[i].position + Vector3.right * Backgrounds[i].bounds.size.x;
            bgAnnexe.AddComponent<SpriteRenderer>().sprite = Backgrounds[i];

            bgPos[i].localScale = Vector3.one * BackgroundScale;
            bgPos[i].position = maCamera.position;
            bgPos[i].position += Vector3.forward * (100 - i);
        }
    }

    void Update() {
        camPosX = maCamera.position.x - camPosX;
        float bgCount = 1f / bgPos.Length;
        for (int i = 0 ; i < bgPos.Length ; i++) {
            bgPos[i].position = new Vector3(bgPos[i].position.x + camPosX * (bgCount * (bgPos.Length - i)), maCamera.position.y, bgPos[i].position.z);
            
            if (maCamera.position.x - bgPos[i].position.x < -Backgrounds[i].bounds.size.x / 2) {
                bgPos[i].position = bgPos[i].position + Vector3.left * Backgrounds[i].bounds.size.x * BackgroundScale;
            }
            if (maCamera.position.x - bgPos[i].position.x > Backgrounds[i].bounds.size.x / 2) {
                bgPos[i].position = bgPos[i].position + Vector3.right * Backgrounds[i].bounds.size.x * BackgroundScale;
            }
            
        }        

        camPosX = maCamera.position.x;
    }
}
