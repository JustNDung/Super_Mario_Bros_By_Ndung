
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody2D;
    
    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    // maxJumpTime là tổng thời gian đi len và thời gian đi xuống.
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); 
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);
    
    public bool isGrounded {get; private set;}
    public bool isJumping {get; private set;}
        
    private float inputAxis;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();
        
        isGrounded = rigidbody2D.Raycast(Vector2.down); 
        if (isGrounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime); 
        // Sau khi su dung MoveToward thì velocity.x = inputAxis * moveSpeed    
        // Sử dụng hàm MoveTowards để thay đổi vẫn tốc từ từ, tránh sự thay đổi đột ngột và giúp chuyển động mượt mà hơn

        if (rigidbody2D.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0;
        }

        if (velocity.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        isJumping = velocity.y > 0f;
        
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            isJumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0 || !Input.GetButton("Jump");

        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(position);

        
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        // Chuyển góc dưới trái của màn hình ((0, 0) trong không gian màn hình) sang tọa độ trong thế giới -> vị trí biên trái trong không gian thế giới
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        // Chuyển góc trên phải của màn hình sang không gian thế giới -> vị trí biên phải trong không gian thế giới
        // ScreenToWordPoint chuyển đổi toaj độ từ không gian màn hình sang không gian thế giới
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        
        rigidbody2D.MovePosition(position);
        // Sử dụng rigidbody2D thay vì chỉnh trực tiếp Transform đảm bảo nhân vật tuân theo những định luật vật lý của Unity
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            //velocity.y = 0f;
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
        
    }
}