using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnerTrigger : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    [SerializeField] private EnemySO[] enemies;
    private bool oneTime = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && oneTime)
        {
            StartCoroutine(WaitSpawnEnemy());
            oneTime = false;
        }
    }
    private IEnumerator WaitSpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            EnemySpawner.Instance.SpawnEnemy(transform.position, enemies[Random.Range(0, enemies.Length)]);
            yield return new WaitForSeconds(0.35f);
        }
        Destroy(gameObject);
    }
}
