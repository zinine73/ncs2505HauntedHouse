using UnityEngine;

public class Key : MonoBehaviour
{
    public string KeyName;
    void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = 
            other.gameObject.GetComponent<PlayerMovement>();
        if (player == null) return;
        player.AddKey(KeyName);
        Destroy(gameObject);
    }
}
