using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Transform healthBar;
    private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        healthBar = transform.Find("HealthBarBg").Find("HealthBar");
        maxHealth = GetComponent<IEnemy>().EnemySO.maxHealth;
        
    }
    public void UpdateHealthBar()
    {
        currentHealth = GetComponent<IEnemy>().HealthHandler.GetCurrentHealth();
        float normalizedHealth = currentHealth / maxHealth;
        healthBar.localScale = new Vector3(normalizedHealth, 1f, 1f);
    }
}
