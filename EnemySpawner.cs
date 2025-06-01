using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private GameObject portalPrefab;
    public void SpawnEnemy(Vector3 spawnPos, EnemySO enemySO)
    {
        Vector3 randomPos = SetRandomPosition(spawnPos);
        StartCoroutine(CreateEnemy(randomPos,enemySO.prefab));

    }
    private IEnumerator CreateEnemy(Vector3 spawnPos,GameObject enemy)
    {
        Vector3 position = new Vector3(spawnPos.x, spawnPos.y - .25f, 0);
        GameObject portal = Instantiate(portalPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(0.75f);
        Instantiate(enemy, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(portal);
    }
    private Vector3 SetRandomPosition(Vector3 spawnPos)
    {
        Vector3 randomPos;
        do
        {
            randomPos = new Vector2(spawnPos.x + Random.Range(-10, 15), spawnPos.y - .5f);
        }
        while (!CheckCollider.Instance.IsGround(randomPos));
        return randomPos;
    }
}
