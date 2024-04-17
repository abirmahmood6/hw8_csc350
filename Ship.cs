// Abir Mahmood
// CSC350H
// Dr. Hao Tang
// ship.cs

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using UnityEngine;

// The Ship class inherits from MonoBehaviour, allowing it to be attached to a Unity GameObject.
public class Ship : MonoBehaviour
{
    // Declarations of private fields for the Ship class.
    private FishCollector fish; // Refers to the FishCollector component attached to the Camera.main GameObject.
    private GameObject targetFish; // Holds the current target fish GameObject the ship is aiming to collect.

    private Vector3 direction; // Direction vector pointing towards the target fish.
    private Rigidbody2D rb2d; // Rigidbody2D component of the ship for physics-based movement.

    // Start method called before the first frame update
    void Start()
    {
        // Assigns the fish field by accessing the FishCollector component from the main camera.
        fish = Camera.main.GetComponent<FishCollector>();
        // Assigns the rb2d field by accessing the Rigidbody2D component attached to the same GameObject as this script.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //// Update method called once per frame
    //void Update()
    //{

    //}

    // Coroutine initiated when the GameObject this script is attached to is clicked.
    IEnumerator OnMouseDown()
    {
        // Continuously loops as long as there are fish in the fishList of the FishCollector.
        while (fish.fishList.Count != 0)
        {
            // Finds and sets the nearest fish as the target.
            findNearestFish();
            // Waits until the ship's movement has almost stopped before proceeding.
            yield return new WaitUntil(() => rb2d.velocity.magnitude < 0.1f);
        }
    }

    // Called when another GameObject remains within a trigger collider attached to the same GameObject as this script.
    void OnTriggerStay2D(Collider2D other)
    {
        // Checks if the collider belongs to the target fish.
        if (other.gameObject == targetFish)
        {
            // Destroys the target fish GameObject.
            Destroy(targetFish);
            // Removes the target fish from the fishList in the FishCollector.
            fish.fishList.Remove(targetFish);
            // Halts the ship's movement.
            rb2d.velocity = Vector2.zero;
            // Logs the collection of a fish.
            Debug.Log("Fish Collected");
        }
    }

    // Finds the nearest fish from the fishList in the FishCollector and designates it as the target.
    void findNearestFish()
    {
        // Assumes the first fish in the list as the nearest fish initially.
        targetFish = fish.fishList.First();
        // Calculates the distance to this initial fish.
        float nearestDistance = Vector3.Distance(transform.position, targetFish.transform.position);

        // Iterates through all fish in the list to find the actual nearest fish.
        foreach (GameObject fish in fish.fishList)
        {
            float comparingDistance = Vector3.Distance(transform.position, fish.transform.position);
            // If a closer fish is found, updates the nearest fish and distance.
            if (comparingDistance < nearestDistance)
            {

                nearestDistance = comparingDistance;
                targetFish = fish;
            }
        }

        // Determines the direction towards the target fish and applies a force to propel the ship in that direction.
        direction = (targetFish.transform.position - transform.position).normalized;
        rb2d.AddForce(direction * 5f, ForceMode2D.Impulse);
    }
}
