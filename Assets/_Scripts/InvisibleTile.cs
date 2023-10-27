using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class InvisibleTile : MonoBehaviour
{   
    [SerializeField] private GameObject invisibleTile;
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private Switch switchButton;
    [SerializeField] private BoxCollider2D hiddenObstacle;

    private void Start()
    {   
        tileSprite = invisibleTile.GetComponent<SpriteRenderer>();
        hiddenObstacle = invisibleTile.GetComponent<BoxCollider2D>();

        switchButton.OnSwitchPress += InvisibleTile_OnSwitchPress;
        switchButton.OnSwitchRelease += InvisibleTile_OnSwitchRelease;

        tileSprite.DOFade(0f, 0f);
    }
    private void InvisibleTile_OnSwitchPress()
    {
        tileSprite.DOFade(1f, 0.5f);
        hiddenObstacle.enabled = false;
    }
    private void InvisibleTile_OnSwitchRelease()
    {
        tileSprite.DOFade(0f, 0.5f);
        hiddenObstacle.enabled = true;
    }

}
