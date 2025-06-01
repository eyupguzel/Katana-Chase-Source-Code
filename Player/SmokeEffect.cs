using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private Transform parent;
    private Animator animator;
    private IPlayerInputService inputService;

    private void Start()
    {
        animator = GetComponent<Animator>();
        parent = transform.parent;
        inputService = parent.GetComponent<PlayerController>().inputService;
    }

    private void Update()
    {
        if (inputService.IsDashPressed && inputService.MovementInput.x != 0 &&IsGrounded())
        {
            animator.SetTrigger("DashSmoke");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(parent.position, Vector2.down, 1.5f, groundLayer);
        if (hit.collider != null && hit.collider.tag == "Ground")
            return true;
        else
            return false;
    }
}
