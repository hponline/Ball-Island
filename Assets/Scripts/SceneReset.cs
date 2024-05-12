using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnFall : MonoBehaviour
{
    public Transform playerTransform; // Oyuncu nesnesinin transform bile�eni
    public float s�n�rY = -50f; // D��me e�i�i

    void Update()
    {
        if (playerTransform.position.y < s�n�rY)
        {
            ResetScene();
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Scene Reset");
        SpawnManager.roundSayac = 1;
    }
}
