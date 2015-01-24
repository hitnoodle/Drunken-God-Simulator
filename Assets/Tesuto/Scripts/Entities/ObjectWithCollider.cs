using UnityEngine;
using System.Collections;

public class ObjectWithCollider : MonoBehaviour
{
    public delegate void Action();
    public Action onCollideWithPlayer;

    void OnTriggerEnter2D(Collider2D coll)
    {
        string objectTag = coll.gameObject.tag;
        if (objectTag == "Player")
        {
            if (onCollideWithPlayer != null)
                onCollideWithPlayer();
        }
    }
}
