using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private List<GameObject> breakablePieces = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // Automatically find all child objects and store them as breakable pieces
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Energy") {
                continue; // Skip adding "Energy" to the breakablePieces list
            }
            breakablePieces.Add(child.gameObject);
            child.gameObject.SetActive(false); // Hide pieces at start
        }
        // foreach(var piece in breakablePieces) {
        //     piece.SetActive(false);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Do something when hit by a bullet
            Debug.Log("Rock was hit by a bullet!");
            Debug.Log("Children: " + breakablePieces.Count);
            Break();
        }
        else if (collision.gameObject.CompareTag("Spaceship"))
        {
            Debug.Log("Collide Spaceship");
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 reflection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
            if (reflection.magnitude < 1f) // Ensure it moves enough
            {
                reflection = reflection.normalized * 2f; // Give it a push
            }
            rb.velocity = reflection;
        }
    }

    public void Break() {
        foreach(var piece in breakablePieces) {
            piece.SetActive(true);
            piece.transform.parent = null;
            Destroy(piece, 3f);
        }
        gameObject.SetActive(false);
    }
}
