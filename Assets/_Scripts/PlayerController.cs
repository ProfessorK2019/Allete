using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private float timeToMove = .2f; // Tốc độ di chuyển của người chơi
    private bool isMoving;
    private Vector3 originPos,targetPos;
    private void Update() {
        if(Input.GetKey(KeyCode.W) && !isMoving){
            StartCoroutine(Move(Vector3.up));
        }
        if(Input.GetKey(KeyCode.S) && !isMoving){
            StartCoroutine(Move(Vector3.down));
        }
        if(Input.GetKey(KeyCode.A) && !isMoving){
            StartCoroutine(Move(Vector3.left));
        }
        if(Input.GetKey(KeyCode.D) && !isMoving){
            StartCoroutine(Move(Vector3.right));
        }
    }
    private IEnumerator Move(Vector3 dir){
        isMoving = true;
        float elapsedTime = 0;
        originPos = transform.position;
        targetPos = originPos + dir;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
