using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    
    public Sprite playerIdle;
    public Sprite playerJump;
    public Sprite playerSlide;
    public AnimatedSprite playerRun;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    
    // LateUpdate được gọi mỗi cuối frame
    private void LateUpdate()
    {
        playerRun.enabled = playerMovement.isRunning;
        
        if (playerMovement.isJumping)
        {
            spriteRenderer.sprite = playerJump;
        } 
        else if (playerMovement.isSliding)
        {
            spriteRenderer.sprite = playerSlide; 
        } 
        else if (!playerMovement.isRunning) 
        {
            spriteRenderer.sprite = playerIdle;
        }
    }
    
    
}
