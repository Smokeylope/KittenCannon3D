using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnucklesMovement: MonoBehaviour { // Liitä Knucklesiin

    public GameObject mainCamera; // Scripti laittaa gameObjectin liikkumaan kameraa päin
    public Rigidbody ukRB;
    public bool hasRb;
    public bool hasDied;
    public int speed;
    public int deathCount;
    public Vector3 origPos;

    public float respawnDelay;
    public AudioSource deathSound;
    public AudioSource breathSound;
    public GameObject loseText;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main.gameObject;
        ukRB = GetComponent<Rigidbody>();
        if (ukRB != null) // Jos Rigidbodya ei loydy, niin gameObjecti liikkuu translatella, AddForcen sijaan.
        {
            hasRb = true;
        }
        else hasRb = false;

        origPos = transform.position;
        respawnDelay = 3f;
        deathCount = 0;
        loseText = GameObject.Find("LoseText");
        loseText.GetComponent<MeshRenderer>().enabled = false;
        breathSound.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(mainCamera.transform.position, gameObject.transform.position) < 3 && !hasDied)
        {
            Die();
            loseText.GetComponent<MeshRenderer>().enabled = true;
            deathCount = 0;

            GameObject[] knuckleses = GameObject.FindGameObjectsWithTag("Knuckles");
            foreach (GameObject k in knuckleses)
            {
                if (gameObject != k)
                {
                    Destroy(k);
                }
            }
        }

        if (hasDied)
        {
            transform.RotateAround(transform.position, Vector3.up, 2000 * Time.deltaTime);
            respawnDelay -= Time.deltaTime;
            if (respawnDelay <= 0)
            {
                Respawn();
            }
            return;
        }

        if (hasRb)
        {
            Vector3 dir = (mainCamera.transform.position - gameObject.transform.position).normalized;

            ukRB.AddForce(dir * speed, ForceMode.Acceleration);
        } else
        {
            Vector3 dir = (gameObject.transform.position - mainCamera.transform.position).normalized;

            Vector3 targetDir = mainCamera.transform.position - transform.position;
            float step = 50 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.Translate(Vector3.forward/50 * speed);
        }
    }

    public void Die()
    {
        hasDied = true;
        breathSound.Stop();
        deathSound.Play();
        respawnDelay = 3f;
        Vector3 newRotation = new Vector3(0, 0, 90);
        transform.RotateAround(transform.position, newRotation, 90);
        deathCount++;
    }

    public void Respawn()
    {
        //transform.position = origPos + new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25));
        transform.position = mainCamera.transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Vector3 dir = (gameObject.transform.position - mainCamera.transform.position).normalized;
        transform.position = mainCamera.transform.position + (dir * 250);
        hasDied = false;
        loseText.GetComponent<MeshRenderer>().enabled = false;
        breathSound.Play();
        GameObject[] knuckleses = GameObject.FindGameObjectsWithTag("Knuckles");

        if (deathCount > 4 && knuckleses.Length < 3)
        {
            deathCount = 0;
            GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity);
            clone.GetComponent<KnucklesMovement>().Respawn();
        }
    }

    private void OnCollisionEnter(Collision col) // Huom: kuoleminen vaatii colliderin cannonballille ja knucklesille
    {
        if (col.gameObject.CompareTag("Bullet") && !hasDied) // Cannonballille pitaa myos laittaa "Bullet" tagi
        {
            Die();
            Destroy(col.gameObject);
        }
    }
}
