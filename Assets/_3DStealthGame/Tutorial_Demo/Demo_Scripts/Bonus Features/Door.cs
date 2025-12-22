using System;
using UnityEngine;

namespace StealthGame
{
    public class Door : MonoBehaviour
    {
        public string KeyName;
    
        private void OnCollisionEnter(Collision other)
        {
            PlayerMovement player =
            other.gameObject.GetComponent<PlayerMovement>();

            if (player == null)
                return;

            if (player.OwnKey(KeyName))
            {
                Destroy(gameObject);
            }
        }
    }
}