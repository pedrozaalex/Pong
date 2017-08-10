using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPlayerTwo : MonoBehaviour
{
    
    // The Transform component for the object to be controlled:
    Transform controlledObject;

    float move;

    // Value used for adjusting the racket movement sensitivity settings
    public float sensitivity;
    public float maxVerticalPosition;

    private void Awake()
    {
    }

    // Use this for initialization
    private void Start()
    {
        controlledObject = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // For receiving user input
        if (Input.GetKey(KeyCode.UpArrow))
            move = 1f;
        else
            if (Input.GetKey(KeyCode.DownArrow))
            move = -1f;

        // To apply the sensitivity settings, we multiply the move value
        // we multiply it by a constant float s such as 0 < s < 1 
        move *= sensitivity;

        move *= Time.deltaTime;

        // We finally apply the input received to the position of the racket
        controlledObject.position = new Vector3(controlledObject.position.x,
            controlledObject.position.y + move, controlledObject.position.z);

        // The rackets can't go out of bounds, so we gotta clamp the Y value of the position
        float yPosition = Mathf.Clamp(controlledObject.position.y, -maxVerticalPosition, maxVerticalPosition);
        controlledObject.position = new Vector3(controlledObject.position.x,
            yPosition, controlledObject.position.z);
    }
    
}
