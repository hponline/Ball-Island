using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // FocalPoint'i odak noktas� al�p onun etraf�nda d�n�yoruz
    private GameObject focalPoint;
    private Rigidbody playerRb;
    private float powerUpStrangth = 30.0f;
    public float speed = 5.0f;
    public bool hasPowerUp = false;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // Player nesnemizin alt�ndaki �ember
        powerupIndicator.transform.position = transform.position + new Vector3(0, - 0.4f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            powerupIndicator.gameObject.SetActive(true);
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // G��lendiricimizi ald�ktan sonra 7sn bekler ve ba�a d�ner.
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            // D��man nesnenin collision de�erini al�r ve de�i�kene atar.
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            // D��man nesnesi konumundan player konumunu ��kart�r ve de�i�kene atar.
            Vector3 awayFromPlayer = enemyRigidBody.transform.position - transform.position;

            // AddForce methodu ile itme/d�rtme ger�ekle�tirilir.
            enemyRigidBody.AddForce(awayFromPlayer * powerUpStrangth, ForceMode.Impulse);


            Debug.Log("Player g��lendirme ile : " + collision.gameObject.name + " nesnesine �arpt� " + hasPowerUp);
        }
    }
}
