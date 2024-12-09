using UnityEngine;
using System.Collections;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AddCoin();
        StartCoroutine(Animate()); 
    }
    
    private IEnumerator Animate()
    {
        // TODO
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;
        
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        
        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsedTime = 0f;
        float duration = 0.25f;

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
