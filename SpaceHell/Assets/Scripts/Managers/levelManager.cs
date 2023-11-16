using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    public int enemiesKilled = 0;

    int totalEnemiesToSpawn = 3; // Change this to the total number of enemies you want to spawn
    int enemiesCreated = 0;

    bool bossCreated = false;
    bool bossKilled = false;

    // Set the interval between enemy creations
    float enemyCreationInterval = 8f; // Change this to the desired interval in seconds
    float nextEnemyCreationTime = 0f;

    void Update()
    {
        if (TimeManager.Minute == 10 && enemiesCreated < totalEnemiesToSpawn && Time.time > nextEnemyCreationTime)
        {
            StartCoroutine(CreateEnemy());
            nextEnemyCreationTime = Time.time + enemyCreationInterval;
        }
        else if (TimeManager.Minute > 1 && TimeManager.Minute % 20 == 0 && enemiesCreated < totalEnemiesToSpawn && Time.time > nextEnemyCreationTime)
        {
            StartCoroutine(CreateEnemy());
            nextEnemyCreationTime = Time.time + enemyCreationInterval;
        }
        else if (enemiesKilled == totalEnemiesToSpawn && !bossCreated)
        {
            StartCoroutine(CreateBoss());
            bossCreated = true;
        }
        else if (bossKilled)
        {
            // showWinScreen();
        }
    }

    IEnumerator CreateEnemy()
    {
        Vector3 enemyStartPosition = new Vector3(0f, 25f, 100f);
        GameObject enemy = Instantiate(enemyPrefab, enemyStartPosition, enemyPrefab.transform.rotation);

        float elapsedTime = 0f;
        float moveDuration = 2f;

        Vector3 enemyFinalPosition = GetEnemyFinalPosition();

        enemiesCreated++;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            enemy.transform.position = Vector3.Lerp(enemyStartPosition, enemyFinalPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.transform.position = enemyFinalPosition;
    }


    Vector3 GetEnemyFinalPosition()
    {
        return new Vector3(5f * enemiesCreated, 25f, 10f * enemiesCreated);
    }

    IEnumerator CreateBoss()
    {
        yield return new WaitForSeconds(1f);

        Vector3 bossStartPosition = new Vector3(0f, 25f, 15f);
        GameObject boss = Instantiate(bossPrefab, bossStartPosition, Quaternion.Euler(90f, 0f, 0f));

        float elapsedTime = 0f;
        float moveDuration = 5f; // Adjust the duration as needed
        Vector3 bossFinalPosition = new Vector3(10f, 25f, 20f); // Set the final position for the boss

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            boss.transform.position = Vector3.Lerp(bossStartPosition, bossFinalPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boss.transform.position = bossFinalPosition;
    }

}
