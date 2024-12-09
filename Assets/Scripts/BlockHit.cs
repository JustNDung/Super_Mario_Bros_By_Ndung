using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public int maxHits = -1;
    public Sprite emptyBlock;
    private bool animating;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 &&  collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        
        maxHits--;
        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;
        // TODO
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;
        
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsedTime = 0f;
        float duration = 0.125f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        transform.localPosition = to;
    }
}
