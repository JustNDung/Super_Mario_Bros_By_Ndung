using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            GameManager.instance.ResetLevel(3f);
        }
        else
        {
            Destroy(collision.gameObject);
        }
        
        
    }
}
