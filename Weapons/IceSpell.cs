using UnityEngine;

public class IceSpell : MonoBehaviour,IWeapon
{
    [SerializeField] private float damage;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;

    public IAttack attackState;

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        if (transform.parent.localScale.x < 0)
        {
            rotationSpeed = -rotationSpeed;
            movementSpeed = -movementSpeed;
        }
        transform.SetParent(null, true);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime,Space.World);
        if (Mathf.Abs(transform.position.x - startPosition.x) > attackRange)
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
    }
}
