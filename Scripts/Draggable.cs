using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    TapHandler TH;

    bool grabbed; 

    float basex, basey;

    Collider2D currentCollision;

    void Start()
    {
        basex = transform.position.x;
        basey = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed){
            Vector2 c = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = c;
            if (transform.position.y >= 5.6 || transform.position.y <= -5.6 || transform.position.x <= -3 || transform.position.x >= 3){
                Release();
            } else if (Input.GetMouseButtonUp(0)){
                if (currentCollision != null){
                    if (currentCollision.gameObject.name == "DropField"){
                        GameFlow.GF.finalSequence();
                    } else {
                        Release();
                    }
                } else {
                    Release();
                }
            }
        }
    }

    public void PickUp(){
        grabbed = true;
    }

    void Release(){
        transform.position = new Vector3(basex, basey, 0);
        grabbed = false;
        GameFlow.GF.reenableDragAnim();
    }

    void OnTriggerEnter2D(Collider2D collision){
        currentCollision = collision; 
    }

    void OnTriggerStay2D(Collider2D collision){
        currentCollision = collision;
    }

    void OnTriggerExit2D(){
        currentCollision = null;
    }


}
