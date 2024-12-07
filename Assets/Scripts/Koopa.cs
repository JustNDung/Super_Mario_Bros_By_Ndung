using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        } else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shelled && collision.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new (transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = collision.GetComponent<Player>();
                player.Hit();
            }
        }
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;
        
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        entityMovement.direction = direction.normalized;
        
        entityMovement.speed = shellSpeed;
        entityMovement.enabled = true;
        
        gameObject.layer = LayerMask.NameToLayer("Shell");
        // Để shell có thể va chạm và huỷ diệt enemies
    }

    private void EnterShell()
    {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false; 
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }
    
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }
}
