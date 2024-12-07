
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    
    private Rigidbody2D rigidbody2d;
    private Vector2 velocity; 

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    // OnBecameVisible được gọi khi entity xuất hiện trong camera
    private void OnBecameVisible()
    {
        enabled = true;
    }

    // OnBecameInvisible được gọi khi entity ra khỏi visual của camera
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    // 2 above event function được dùng để tối ưu hoá hiệu suất

    private void OnEnable()
    {
        rigidbody2d.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody2d.linearVelocity = Vector2.zero;
        rigidbody2d.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
        
        rigidbody2d.MovePosition(rigidbody2d.position + velocity * Time.fixedDeltaTime);

        if (rigidbody2d.Raycast(direction))
        {
            direction = -direction;
            // Check if có vật cản nào trong hướng di chuyển không nếu có thì di chuyển ngược lại
        }

        if (rigidbody2d.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f); 
            // Ngăn thực thể rơi xuyên mặt đất
        }
    }
    
}
