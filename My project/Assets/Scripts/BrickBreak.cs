using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    // Public variable for the destroyed sprite, set in the Inspector.
    public Sprite explodedBlock;
    
    // Reference to the SpriteRenderer component.
    private SpriteRenderer sr;
    
    // Reference to the Collider2D component.
    private Collider2D col; 

    // Variable to store the original desired size of the brick in world units.
    private Vector3 originalWorldSize; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>(); // Get the collider component
        
        // Capture the original size of the brick in world units before breaking.
        originalWorldSize = sr.bounds.size;
    }

    void Update()
    {
        // Update is currently empty.
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the colliding object is the Player AND the collision point is below the brick's center.
        if (other.gameObject.CompareTag("Player") && other.GetContact(0).point.y < transform.position.y)
        {
            // 1. Swap the sprite to the broken one.
            sr.sprite = explodedBlock;
            
            // 2. *** THE SCALE FIX IS HERE ***
            // Calculate the required scale factor to make the new sprite match the original size.
            // newScale = (originalWorldSize / newSpriteCurrentSize) * currentLocalScale
            
            // Get the current size of the GameObject after swapping the sprite (it's now too small).
            Vector3 currentSize = sr.bounds.size;
            
            // Calculate the necessary factor to stretch the current, small sprite to the original size.
            float scaleFactorX = originalWorldSize.x / currentSize.x;
            float scaleFactorY = originalWorldSize.y / currentSize.y;

            // Apply the necessary scale factors to the GameObject's localScale.
            transform.localScale = new Vector3(
                transform.localScale.x * scaleFactorX, 
                transform.localScale.y * scaleFactorY, 
                transform.localScale.z
            );
            
            // 3. Disable the collider so the player doesn't hit it again or stand on the broken block.
            if (col != null)
            {
                col.enabled = false;
            }

            Debug.Log("Brick Hit and Broken (Forced Scale Applied)");

            // 4. Destroy the GameObject after 2 seconds.
            Object.Destroy(gameObject, 2f);
        }
    }
}