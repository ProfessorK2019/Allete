using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 initialPosition;
    private Tween jumpTween;
    [Header("TILEMAP")]
    [SerializeField] private Grid gridmap;
    [SerializeField] private LayerMask tileLayer;
    private float gridSizeX;
    private float gridSizeY;
    [Header("DOTWEEN JUMP ANIMATION")]
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private int amountOfJump = 1;
    [SerializeField] private float durationOfJump = 0.1f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private void Start()
    {
        gridSizeX = gridmap.cellSize.x + gridmap.cellGap.x;
        gridSizeY = gridmap.cellSize.y + gridmap.cellGap.y;
    }
    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (!isMoving)
        {
            float xPos = transform.position.x;
            float yPos = transform.position.y;

            if (Input.GetKeyDown(KeyCode.D))
                JumpTo(xPos + gridSizeX * 0.5f, yPos - gridSizeY * 0.5f);
            else if (Input.GetKeyDown(KeyCode.A))
                JumpTo(xPos - gridSizeX * 0.5f, yPos + gridSizeY * 0.5f);
            else if (Input.GetKeyDown(KeyCode.S))
                JumpTo(xPos - gridSizeX * 0.5f, yPos - gridSizeY * 0.5f);
            else if (Input.GetKeyDown(KeyCode.W))
                JumpTo(xPos + gridSizeX * 0.5f, yPos + gridSizeY * 0.5f);
        }
    }

    private void JumpTo(float targetX, float targetY)
    {
        isMoving = true;
        targetPosition = new Vector3(targetX, targetY, 0f);

        // Check collider
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position,
        targetPosition - transform.position,
        Vector3.Distance(transform.position, targetPosition),
        tileLayer);

        if (hit.collider == null) //not detected collider
        {
            transform.DOJump(targetPosition, jumpPower, amountOfJump, durationOfJump)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => isMoving = false);
        }
        else // detected collider
        {

            JumpAHalf();
        }
    }
    private void JumpAHalf()
    {
        float jumpTweenDelay = 0.05f;
        jumpTween = transform.DOJump(targetPosition, jumpPower, amountOfJump, durationOfJump)
            .SetEase(Ease.OutCubic);

        StartCoroutine(StopJumpAfterDelay(jumpTweenDelay));
    }
    private IEnumerator StopJumpAfterDelay(float delay)//break jumpAnim when met collider
    {   
        
        initialPosition = transform.position;
        yield return new WaitForSeconds(delay);
        
        //0.3f is duration when comback to initialPosition
        jumpTween.Kill();
        transform.DOMove(initialPosition, 0.3f).OnComplete(() => isMoving = false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 rayDirection = targetPosition - transform.position;
        Gizmos.DrawLine(player.transform.position, transform.position + rayDirection);
    }
}
