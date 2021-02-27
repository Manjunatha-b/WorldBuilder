using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour
{

    private Animator anim;
	private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    private Vector3 lastSafe;

    public int speed = 13;
	public float turnSpeed = 1000.0f;
    public float gravity = 20.0f;

    public GameObject invisButton;
    bool isMoving;

    private Vector2 initpos;
    private Vector2 currpos;

    void Start(){

        Button tempButton = invisButton.GetComponent<Button>();
        EventTrigger trigger = tempButton.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => setInitPos());
        trigger.triggers.Add(pointerDown);
        
        isMoving = false;
        initpos = Vector2.zero;
        currpos = Vector2.zero;

        controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();

        lastSafe = transform.position;
    }

    void setInitPos(){
        initpos = Input.mousePosition;
        isMoving = true;
    }

    void Update(){
        if(isMoving && Input.GetMouseButton(0)){
            currpos = Input.mousePosition;
            Vector2 inputs = (currpos-initpos).normalized;
            Vector3 inputs3 = new Vector3(inputs[0],0,inputs[1]);
            anim.SetInteger ("AnimationPar", 1);

            if(controller.isGrounded){
			    moveDirection = inputs3*speed;
                lastSafe =transform.position;
            }
		    
            Vector3 obj = new Vector3(transform.forward.x,0,transform.forward.z);
            transform.LookAt(transform.position+inputs3*20);
        }
        else{
            isMoving = false;
            moveDirection = Vector3.zero;
            anim.SetInteger ("AnimationPar", 0);
        }
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
        if(transform.position.y<-0.05)
            resetPos();
    }

    void resetPos(){
        controller.SimpleMove(Vector3.zero);
        transform.position = lastSafe;
    }
}
