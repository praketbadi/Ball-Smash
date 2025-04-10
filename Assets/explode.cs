using UnityEngine;

public class ExplodeOnCollision2D : MonoBehaviour
{
    public GameObject explosionEffect;
    public int pointsAwarded = 1; // Set the points value in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Award points through the scoreman instance
        if (scoreman.instance != null)
        {
            scoreman.instance.AddPoints(pointsAwarded);
        }
        else
        {
            Debug.LogError("scoreman Instance is null! Make sure the scoreman script is attached to a GameObject in the scene.");
        }

        Destroy(gameObject);
    }
}