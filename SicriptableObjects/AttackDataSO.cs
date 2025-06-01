using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackData")]
public class AttackDataSO : ScriptableObject
{
    public int damage;
    public GameObject hitVfx;
    public AudioClip hitSfx;
    public AudioClip attackSfx;
}
