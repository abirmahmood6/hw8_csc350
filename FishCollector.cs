// Abir Mahmood
// CSC350H
// Dr. Hao Tang
// FishCollector.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollector : MonoBehaviour
{
    [SerializeReference] GameObject fishPrefab;

    public List<GameObject> fishList = new List<GameObject>();
    // Start method called before the first frame update

    void Start()
    {

    }

    // Update method called once per frame
    void Update()
    {
        // Checks if the right mouse button (button index 1) was pressed.
        if (Input.GetMouseButtonDown(1))
        {
            // Determines the world position from the mouse's position on the screen.
            Vector3 mousePosition = Input.mousePosition; // Obtains the screen position of the mouse.

            mousePosition.z = -Camera.main.transform.position.z; // Adjusts for camera's z position to ensure accurate world position calculation.
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // Converts the screen position to a world position.

            // Creates a new fish GameObject using the fishPrefab.
            GameObject fish = Instantiate<GameObject>(fishPrefab);
            fish.transform.position = worldPosition; // Sets the position of the newly created fish to the calculated world position.

            fishList.Add(fish); // Appends the newly created fish to the fishList for monitoring.
        }
    }
}
