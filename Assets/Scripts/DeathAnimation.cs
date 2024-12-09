using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    private int sortingOrderLyer = 10;

    // Khi nhấn Reset trong Inspector, tự động gán spriteRenderer với thành phần SpriteRenderer trên cùng đối tượng.
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    { 
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = sortingOrderLyer;
        if (deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite;
        }  
    }

    // Tắt hệ thống vật lý khi thực hiện hoạt ảnh chết.
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    // Thực hiện hoạt ảnh nhảy lên trước khi rơi xuống mô phỏng "chết".
    private IEnumerator Animate()
    {
        float elapsedTime = 0f;
        float duration = 3f;
        
        float jumpVelocity = 10f;
        float gravity = -36f;
        
        Vector3 velocity = Vector3.up * jumpVelocity;
        while (elapsedTime < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsedTime += Time.deltaTime; 
            yield return null;
        }
    }
    
}
