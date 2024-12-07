using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer smallRenderer;
    public SpriteRenderer bigRenderer;
    
    private DeathAnimation deathAnimation;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool death => deathAnimation.enabled;
    
    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit()
    {
        if (big)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    private void Shrink()
    {
        
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        
        GameManager.instance.ResetLevel(3f);
        // Thời gian reset phải bằng với thời gian thực hienej của deathAnimation
    }
}
