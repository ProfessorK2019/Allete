using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerInput playerInput;
    protected Vector3 initialPosition;
    protected Vector3 targetPosition;
    [SerializeField] protected Transform rayCastStartPoint;
    [Header("TILEMAP")]
    [SerializeField] protected LayerMask tileLayer;

    [Header("DOTWEEN JUMP ANIMATION")]
    protected Tween jumpTween;
    protected bool isMoving = false;
    [SerializeField] protected float jumpPower = 1f;
    [SerializeField] protected int amountOfJump = 1;
    [SerializeField] protected float durationOfJump = 0.1f;
    [Header("SPRITE CHECK")]
    protected bool isSameSprite;
    [SerializeField] protected Sprite illegalSprite;
    [Header("PARTICLE")]
    [SerializeField] protected GameObject particlePrefab;

    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    protected virtual void Update()
    {
        GatherInput();
    }
    protected virtual void GatherInput()
    {
        if (GameManager.Instance.IsPlaying())
        {
            if (!isMoving)
            {
                float xPos = transform.position.x;
                float yPos = transform.position.y;

                if (playerInput.Right())
                    JumpTo(xPos + GridManager.Instance.gridSizeX * 0.5f, yPos - GridManager.Instance.gridSizeY * 0.5f);
                else if (playerInput.Left())
                    JumpTo(xPos - GridManager.Instance.gridSizeX * 0.5f, yPos + GridManager.Instance.gridSizeY * 0.5f);
                else if (playerInput.Down())
                    JumpTo(xPos - GridManager.Instance.gridSizeX * 0.5f, yPos - GridManager.Instance.gridSizeY * 0.5f);
                else if (playerInput.Up())
                    JumpTo(xPos + GridManager.Instance.gridSizeX * 0.5f, yPos + GridManager.Instance.gridSizeY * 0.5f);
            }
        }
    }
    protected virtual void JumpTo(float targetX, float targetY)
    {
        isMoving = true;
        targetPosition = new Vector3(targetX, targetY, 0f);

        // Check collider
        RaycastHit2D hit = Physics2D.Raycast(rayCastStartPoint.transform.position,
        targetPosition - transform.position,
        Vector3.Distance(transform.position, targetPosition),
        tileLayer);
        if (hit.collider == null) //not detected collider
        {
            StartCoroutine(CheckGridSprite());
            if (!isSameSprite)
            {
                EventManager.PlayerMove();
                jumpTween = transform.DOJump(targetPosition, jumpPower, amountOfJump, durationOfJump)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => isMoving = false);
            }
        }
        else // detected collider
        {
            StartCoroutine(StopJumpAfterDelay());
        }
    }
    protected virtual IEnumerator StopJumpAfterDelay()//break jumpAnim when met collider
    {
        float jumpTweenDelay = 0.05f;
        initialPosition = transform.position;

        jumpTween = transform.DOJump(targetPosition, jumpPower, amountOfJump, durationOfJump)
            .SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(jumpTweenDelay);

        jumpTween.Kill();
        jumpTween = transform.DOJump(initialPosition, jumpPower, amountOfJump, durationOfJump)
            .SetEase(Ease.OutCubic).OnComplete(() => isMoving = false);

        CameraShake.Instance.Shake();
        EventManager.PlayerBarrier();
    }
    protected virtual IEnumerator CheckGridSprite()
    {
        Vector3Int gridPos = GridManager.Instance.GetGridMap().WorldToCell(targetPosition);
        Sprite gridSprite = GridManager.Instance.tileMap.GetSprite(gridPos);
        if (illegalSprite.name == gridSprite.name)
        {
            isSameSprite = true;
            StartCoroutine(StopJumpAfterDelay());
        }
        yield return new WaitForSeconds(0.1f);
        // isMoving = false;
        isSameSprite = false;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
    protected virtual void SpawnParticle() => Instantiate(particlePrefab, transform.position, Quaternion.identity);

}
