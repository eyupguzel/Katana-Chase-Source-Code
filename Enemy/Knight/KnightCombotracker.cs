using System;
using System.Collections;
using UnityEngine;

public class KnightCombotracker : MonoBehaviour
{
    private int comboCount;
    [HideInInspector] public GameObject attackAlert;

    private void Awake()
    {
        attackAlert = transform.Find("AttackAlert").gameObject;
        attackAlert.SetActive(false);
    }

    public IEnemyState GetCurrentAttackState(int comboCount, IEnemy enemy, Animator animator, EnemyStateMachine enemyState)
    {
        switch (comboCount)
        {
            case 0 : return new KnightAttack_1_State(enemy, animator, enemyState);
            case 1 : return new KnightAttack_2_State(enemy, animator, enemyState);
            case 2 : return new KnightAttack_3_State(enemy, animator, enemyState);
            default: return null;
        };
    }
}
