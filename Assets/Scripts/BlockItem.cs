using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Ẩn vật thể và tắt va chạm
        rb.bodyType = RigidbodyType2D.Kinematic;
        circleCollider.enabled = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        
        yield return new WaitForSeconds(0.25f);
        
        // Hiển thị lại sau khoảng thời gian 0.25s
        spriteRenderer.enabled = true;
        
        // Di chuyển vật thể lên trên trong 0.5s
        float elapsedTime = 0f;
        float duration = 0.5f;
        
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime; 
            yield return null;
        }
        
        transform.localPosition = endPosition;
        
        rb.bodyType = RigidbodyType2D.Dynamic;
        circleCollider.enabled = true;
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
    }
}
