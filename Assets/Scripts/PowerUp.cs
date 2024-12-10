using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] float starpowerDuration = 10f;
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        StarPower,
    }
    
    public Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.instance.AddLife();
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.StarPower:
                player.GetComponent<Player>().Starpower(starpowerDuration);
                break;
                
        }
        Destroy(gameObject); 
    }
}
