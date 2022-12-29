using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;

    public Transform followTarget;

    // Starting position of the parallax object.
    Vector2 startingPos;

    // Starting Z value of the parallax object.
    float startingZ;

    // The distance the camera has moved from that starting position of the parallax object .
    Vector2 camMovedSinceStart => (Vector2) cam.transform.position - startingPos;

    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    // If object is in front of the player, use nearClipPane. Otherwise, use farClipPane.
    float clippingPane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // The further the object from the player, the faster the parallax object will move.
    // Drag it's z-value closer to the target to make it move slower.
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPane;

    // Start is called before the first frame update.
    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;        
    }

    // Update is called once per frame
    void Update()
    {
       // When the target moves, move the parallax object [the same distances * a multiplier].
       Vector2 newPosition = startingPos + camMovedSinceStart * parallaxFactor; 

        // The X/Y position changes based on target's travel speed times the parallax factor, but Z stays consistent.
       transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
