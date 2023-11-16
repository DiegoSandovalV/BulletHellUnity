using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    LiveManager liveManager;
    BulletCounter bulletCounter;

    void Awake()
    {
        bulletCounter = FindObjectOfType<BulletCounter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (bulletCounter != null)
        {
            bulletCounter.bulletCount++;
            Destroy(gameObject, 10f);
        }
        else
        {
            Debug.LogError("BulletCounter not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
        if (bulletCounter != null)
        {
            bulletCounter.bulletCount--;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            liveManager = FindObjectOfType<LiveManager>();
            if (liveManager != null)
            {
                liveManager.SendMessage("TakeDamage");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("LiveManager not found");
            }
        }
    }

}
