using System.Collections;
using UnityEngine;

public class Knife : MonoBehaviour, IWeapon
{
    [SerializeField] private float damage;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;

    public IAttack attackState;

    private float startPositionX;

    private void Start()
    {
        startPositionX = transform.position.x;

        if(transform.parent.localScale.x < 0)
        {
            rotationSpeed = -rotationSpeed;
            movementSpeed = -movementSpeed;
        }
    }
    private void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed);
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x - startPositionX) > attackRange)
            Destroy(gameObject);

    }
    public void SetAttackState(IAttack attackState)
    {
        this.attackState = attackState;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemy = collision.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(attackState, (int)damage);
            Destroy(gameObject);
        }
        else
            Debug.Log("No enemy found");
    }
}
