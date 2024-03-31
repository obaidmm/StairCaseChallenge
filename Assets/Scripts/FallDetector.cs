using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetector : MonoBehaviour
{
    [SerializeField]
    private string sceneNameToLoad = "Popup"; // Replace with your scene name

    // Attach this script to your Player object, not the Falling Ground
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the Falling Ground
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with the trigger, loading scene: " + sceneNameToLoad);
            // Load the specified scene
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }

    public void ReturnToStart()
    {
        // Assuming 'startPosition' is the position where the player should return to.
        // This needs to be assigned, for example in Start(), or be a predefined Vector3.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //player.transform.position = startPosition.position; // Replace with the actual start position
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }

}
