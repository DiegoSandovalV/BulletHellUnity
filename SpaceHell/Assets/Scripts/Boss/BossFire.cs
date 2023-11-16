using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    Vector3 bossVelocity;

    bool routineIsRunning = false;
    bool movementIsRunning = false;

    LiveManager liveManager;

    void Awake()
    {
        liveManager = FindObjectOfType<LiveManager>();

        if (liveManager == null)
        {
            Debug.LogError("LiveManager not found in the scene!");
        }
    }


    void Update()
    {

        // Get the boss's current velocity
        Vector3 bossVelocity = GetComponent<Rigidbody>().velocity;

        if (TimeManager.Minute % 4 == 0 && !routineIsRunning)
        {
            routineIsRunning = true;
            StartCoroutine(playRoutine());
        }

        if (TimeManager.Minute % 10 == 0 && !movementIsRunning)
        {
            movementIsRunning = true;
            StartCoroutine(PlayMovement());
        }

    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(playRoutine());
        }
    }

    void createBullet(Vector3 bossVelocity)
    {
        // Set the distance from the boss 
        float distance = 5f;

        // Calculate the position by moving in the up direction (Vector3.up) at the specified distance
        Vector3 bulletPosition = transform.position + Vector3.up * distance;

        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * bulletSpeed + bossVelocity;
    }

    void createBullet(float angle, float speed, Vector3 bossVelocity)
    {
        // Set the distance from the boss 
        float distance = 10f;

        Quaternion rotation = Quaternion.Euler(90f, angle, 0f);

        // Calculate the position by moving in the up direction (rotation * Vector3.up) at the specified distance
        Vector3 bulletPosition = transform.position + rotation * Vector3.up * distance;

        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);

        // Combine the boss's velocity with the bullet's velocity
        Vector3 combinedVelocity = bossVelocity + rotation * Vector3.up * speed;
        bullet.GetComponent<Rigidbody>().velocity = combinedVelocity;
    }



    IEnumerator circularFire()
    {
        for (int i = 0; i < 360; i += 10)
        {
            float distance = 5f;
            Quaternion rotation = Quaternion.Euler(90f, i, 0f);
            Vector3 bulletPosition = transform.position + rotation * Vector3.up * distance;
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
            bullet.GetComponent<Rigidbody>().velocity = rotation * Vector3.up * bulletSpeed + bossVelocity;
        }
        yield return new WaitForSeconds(0.5f);
        routineIsRunning = false;

    }

    IEnumerator spiralFire()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 360; i += 10)
            {
                float distance = 5f;
                Quaternion rotation = Quaternion.Euler(90f, i, 0f);
                Vector3 bulletPosition = transform.position + rotation * Vector3.up * distance;
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
                bullet.GetComponent<Rigidbody>().velocity = rotation * Vector3.up * bulletSpeed + bossVelocity;
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 360; i < 0; i -= 10)
            {
                float distance = 5f;
                Quaternion rotation = Quaternion.Euler(90f, i, 0f);
                Vector3 bulletPosition = transform.position + rotation * Vector3.up * distance;
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
                bullet.GetComponent<Rigidbody>().velocity = rotation * Vector3.up * bulletSpeed + bossVelocity;
                yield return new WaitForSeconds(0.1f);
            }
        }
        routineIsRunning = false;

    }

    IEnumerator radialBurst()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int i = 90; i < 270; i += 10)
            {
                float distance = 5f;
                Quaternion rotation = Quaternion.Euler(90f, i, 0f);
                Vector3 bulletPosition = transform.position + rotation * Vector3.up * distance;
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
                bullet.GetComponent<Rigidbody>().velocity = rotation * Vector3.up * bulletSpeed + bossVelocity;
            }
            yield return new WaitForSeconds(0.3f); // Delay between bursts
        }
        routineIsRunning = false;

    }


    IEnumerator octopusFire()
    {
        float radius = 15f;
        int numBullets = 10;

        int executionTime = 1;

        float speed = 20f;

        for (int iter = 0; iter < executionTime; iter++)
        {

            for (int i = 0; i < 360; i += 10)
            {
                for (int j = 0; j < numBullets; j++)
                {
                    float angle = Mathf.Sin(i * Mathf.Deg2Rad) * radius + j * 360f / numBullets;

                    createBullet(angle, speed, bossVelocity);
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        routineIsRunning = false;

    }


    IEnumerator dynamicFigurePattern()
    {
        float radius = 15f;
        int numBullets = 10;

        int executionTime = 3;

        float speed = 10f;

        for (int iter = 0; iter < executionTime; iter++)
        {

            for (int i = 0; i < 360; i += 10)
            {
                for (int j = 0; j < numBullets; j++)
                {
                    float angle = Mathf.Sin(i * Mathf.Deg2Rad) * radius + j * 360f / numBullets;
                    float angle2 = Mathf.Cos(i * Mathf.Deg2Rad) * radius + j * 360f / numBullets;
                    createBullet(angle, speed, bossVelocity);
                    createBullet(angle2, speed, bossVelocity);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
        routineIsRunning = false;


    }

    IEnumerator lineFire()
    {
        for (int i = 90; i < 270; i += 10)
        {
            float distance = 5f;
            Quaternion rotation = Quaternion.Euler(90f, i, 0f);
            Vector3 bulletPosition = transform.position + rotation * Vector3.up * distance;
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
            bullet.GetComponent<Rigidbody>().velocity = rotation * Vector3.up * bulletSpeed + bossVelocity;
        }
        yield return new WaitForSeconds(0.5f);
        routineIsRunning = false;

    }

    IEnumerator PlayMovement()
    {
        float duration = 2.0f; // Adjust the duration of the movement
        float distance = 25.0f; // Adjust the distance of the movement

        Vector3 startPos = transform.position;
        Vector3 leftPos = new Vector3(startPos.x - distance, startPos.y, startPos.z);
        Vector3 rightPos = new Vector3(startPos.x + distance, startPos.y, startPos.z);

        // Generate a random velocity factor
        float randomVelocityFactor = Random.Range(0.5f, 2.0f);

        // Move to the left
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, leftPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime * randomVelocityFactor; // Apply random velocity
            yield return null;
        }

        // Move to the right
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(leftPos, rightPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime * randomVelocityFactor; // Apply random velocity
            yield return null;
        }

        // Move back to the center
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(rightPos, startPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime * randomVelocityFactor; // Apply random velocity
            yield return null;
        }

        // Reset the flag so the movement can be triggered again
        movementIsRunning = false;
    }



    IEnumerator playRoutine()
    {
        // Generate a random number between 0 and 6
        int pattern = Random.Range(0, 6);


        // switch statement to determine which pattern to play for 10 seconds
        switch (pattern)
        {
            case 0:
                StartCoroutine(circularFire());
                break;
            case 1:
                StartCoroutine(spiralFire());
                break;
            case 2:
                StartCoroutine(radialBurst());
                break;
            case 3:
                StartCoroutine(octopusFire());
                break;
            case 4:
                StartCoroutine(dynamicFigurePattern());
                break;
            case 5:
                StartCoroutine(lineFire());
                break;
        }


        yield return new WaitForSeconds(10f);
    }

    void OnDestroy()
    {
        Debug.Log("Enemy destroyed");
        liveManager.showWin();
    }


}
