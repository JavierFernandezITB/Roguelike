using UnityEngine;

public class FogScroller : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public Transform otherFog; // Reference to the second fog object

    private float textureWidth;

    void Start()
    {
        // Calculate the width of the sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        textureWidth = spriteRenderer.bounds.size.x;
    }

    void Update()
    {
        // Move both fog objects
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        otherFog.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if this fog has moved completely offscreen
        if (transform.position.x <= -textureWidth)
        {
            // Move this fog to the other side of the second fog
            transform.position = new Vector3(otherFog.position.x + textureWidth, transform.position.y, transform.position.z);
        }

        // Check if the other fog has moved completely offscreen
        if (otherFog.position.x <= -textureWidth)
        {
            // Move the other fog to the other side of this fog
            otherFog.position = new Vector3(transform.position.x + textureWidth, otherFog.position.y, otherFog.position.z);
        }
    }
}
