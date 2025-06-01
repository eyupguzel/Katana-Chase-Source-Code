using UnityEngine;

public class ArcherGladiatorComboTracker : MonoBehaviour
{
    [HideInInspector] public GameObject alertObject;

    private void Awake()
    {
        alertObject = transform.Find("AttackAlert").gameObject;
    }
}
