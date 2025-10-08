using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveTime = 0.2f;         // time to slide across 1 tile
    [SerializeField] private float holdRepeatDelay = 0.25f; // pause before auto-walk
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private bool isMoving = false;
    private Vector3 targetPos;
    private Vector3Int gridPosition;
    private Vector2Int lastDirection;
    private Vector2Int facingDirection = Vector2Int.down; // default facing down
    private float holdTimer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];

        // Initialize grid position based on current tile
        gridPosition = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(gridPosition); // Force snap
        targetPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();

            if (input != Vector2.zero)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                    lastDirection = new Vector2Int((int)Mathf.Sign(input.x), 0);
                else
                    lastDirection = new Vector2Int(0, (int)Mathf.Sign(input.y));

                facingDirection = lastDirection;
                UpdateFacingAnimation(facingDirection);

                if (TryMove(lastDirection))
                {
                    holdTimer = holdRepeatDelay;
                }
            }
        }
        else
        {
            float maxDelta = (1f / moveTime) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, maxDelta);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }

        // Auto movement when holding key
        if (!isMoving && lastDirection != Vector2Int.zero && moveAction.ReadValue<Vector2>() != Vector2.zero)
        {
            holdTimer -= Time.deltaTime;
            if (holdTimer <= 0f)
            {
                facingDirection = lastDirection;
                UpdateFacingAnimation(facingDirection);

                if (TryMove(lastDirection))
                {
                    holdTimer = moveTime;
                }
            }
        }
    }

    private bool TryMove(Vector2Int direction)
    {
        Vector3Int nextGrid = gridPosition + new Vector3Int(direction.x, direction.y, 0);

        if (CanMove(nextGrid))
        {
            gridPosition = nextGrid; // ? Logical position updated immediately
            targetPos = groundTilemap.GetCellCenterWorld(gridPosition); // ? Snap to center
            isMoving = true;
            return true;
        }
        return false;
    }

    private bool CanMove(Vector3Int nextGrid)
    {
        return groundTilemap.HasTile(nextGrid) && !collisionTilemap.HasTile(nextGrid);
    }

    private void UpdateFacingAnimation(Vector2Int dir)
    {
        // TODO: Hook to animator
        // animator.SetFloat("MoveX", dir.x);
        // animator.SetFloat("MoveY", dir.y);
        // animator.SetBool("IsMoving", isMoving);
    }
}
