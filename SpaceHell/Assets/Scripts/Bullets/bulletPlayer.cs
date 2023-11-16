using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlayer : MonoBehaviour
{
    BulletCounter bulletCounter;

    // Start is called before the first frame update
    void Start()
    {

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

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnemyCube")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Boss")
        {
            other.gameObject.SendMessage("TakeDamage");
            Destroy(gameObject);

        }
    }
}
