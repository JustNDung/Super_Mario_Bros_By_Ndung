using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    
    public Sprite playerIdle;
    public Sprite playerJump;
    public Sprite playerSlide;
    public Sprite playerRun;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (playerMovement.isJumping)
        {
            spriteRenderer.sprite = playerJump;
        } 
        else if (playerMovement.isSliding)
        {
            spriteRenderer.sprite = playerSlide; 
        } 
        else if (playerMovement.isRunning)
        {
            spriteRenderer.sprite = playerRun; 
        } 
        else
        {
            spriteRenderer.sprite = playerIdle;
        }
    }
    
    
}
