using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciriclespawner : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject Item;
    public float radius = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomspawn();
        }
    }

    void randomspawn()
    {
        // Generate a random point within a circle on the XY plane
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        // Instantiate the item at the random position, keeping the Z-axis at the spawner's Z
        Vector3 spawnPosition = new Vector3(randomCircle.x, randomCircle.y, transform.position.z);
        GameObject spawnedObject = Instantiate(Item, spawnPosition, Quaternion.identity);

        // Add a Box Collider to the spawned object
        AddBoxCollider(spawnedObject);
    }

    void AddBoxCollider(GameObject spawnedObject)
    {
        // Check if the object already has a Collider
        Collider existingCollider = spawnedObject.GetComponent<Collider>();

        // If it has a Collider, you might want to disable it or remove it
        if (existingCollider != null)
        {
            // Option 1: Disable the existing collider
            // existingCollider.enabled = false;

            // Option 2: Remove the existing collider
            Destroy(existingCollider);
        }

        // Add a Box Collider
        BoxCollider boxCollider = spawnedObject.AddComponent<BoxCollider>();

        // You can further customize the Box Collider here, e.g., its size, center, etc.
        // boxCollider.size = new Vector3(1f, 1f, 1f);
        // boxCollider.center = new Vector3(0f, 0.5f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

     void OnTriggerEnter2D()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}