using UnityEngine;

public class DemonSamuraiComboCounter : MonoBehaviour
{
    public IEnemyState GetCurrentEnemyState(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine, int comboCount)
    {
        switch (comboCount)
        {
            case 0:
                return new DemonSamuraiAttack1State(enemy, animator, stateMachine);
            case 1:
                return new DemonSamuraiAttack2State(enemy, animator, stateMachine);
            case 2:
                return new DemonSamuraiAttack3State(enemy, animator, stateMachine);
            case 3:
                return new DemonSamuraiShoutState(enemy, animator, stateMachine);
            case 4:
                return new DemonSamuraiJumpAttackState(enemy, animator, stateMachine);
            default:
                return new DemonSamuraiIdleState(enemy, animator, stateMachine);
        }
    }
}
