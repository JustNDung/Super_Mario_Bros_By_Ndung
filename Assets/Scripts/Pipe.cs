
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform) );
            }
            
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        // Đảm bảo player không thể di chuyển khi vào ống.
        
        Vector3 enteredPosition = transform.position + enterDirection;
        // Vij trí mà player di chuyển đến
        Vector3 enteredScale = Vector3.one * 0.5f;
        // Giảm kích thước của player đi 50% khi vào ống
        
        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < -3f;
        Camera.main.GetComponent<SlideScrolling>().SetUnderground(underground);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        
        
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsedTime = 0;
        float duration = 1f;
        
        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            
            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        player.position = endPosition;
        player.localScale = endScale;
    }
}
