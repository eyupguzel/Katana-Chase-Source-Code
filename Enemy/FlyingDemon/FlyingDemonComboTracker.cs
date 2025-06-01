using UnityEngine;

public class FlyingDemonComboTracker : MonoBehaviour
{
    [HideInInspector] public GameObject alertObject;

    private void Awake()
    {
        alertObject = transform.Find("AttackAlert").gameObject;
        alertObject.SetActive(false);
    }
    public IEnemyState GetCurrentAttackState(IEnemy enemy,Animator animator,EnemyStateMachine enemyStateMachine)
    {
         return new FlyingDemonAttackState(enemy, animator, enemyStateMachine);
    }
}
