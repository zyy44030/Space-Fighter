using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set In Inspector")]
    public float radius;
    public bool keepOnScreen= true;
    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;
    private void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        offRight = offLeft = offUp = offDown = false;  
    }
    private void LateUpdate() {
        Vector3 pos = transform.position;
        isOnScreen = true;
        if(pos.x > camWidth - radius){
            pos.x = camWidth - radius;
            offRight = true;
        }
        if(pos.x < -camWidth + radius){
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if(pos.y > camHeight - radius){
            pos.y = camHeight - radius;
            offUp = true;
        }
        if(pos.y < -camHeight + radius){
            pos.y = -camHeight + radius;
            offDown = true;
        }
        isOnScreen = !(offRight||offLeft||offUp||offDown);
        if(keepOnScreen && !isOnScreen){
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false; 
        }
    }
}
