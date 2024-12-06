using UnityEngine;

public class SlideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = Mathf.Max(cameraPos.x, player.position.x);
        //cameraPos.x = player.position.x;
        transform.position = cameraPos;
    } 
    
}
