using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform targetPortal;
    [SerializeField] private float animDuration = .2f;
    private float scaleValue = .3f;
    private Vector3 originScale;
    private void Start()
    {
        originScale = transform.localScale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);

            StartCoroutine(ScaleEffect(animDuration));

            TeleportPlayer(other.transform);
        }
    }
    private void TeleportPlayer(Transform player)
    {
        Vector3 _targetPoint = GridManager.Instance.GetGridCenterPosition(targetPortal.position);
        player.position = _targetPoint;
        player.gameObject.SetActive(true);
        CameraController.Instance.ZoomOut();

    }
    private IEnumerator ScaleEffect(float duration)
    {
        transform.DOScale(scaleValue, duration);
        yield return new WaitForSeconds(duration);
        transform.DOScale(originScale, duration);
    }
}
