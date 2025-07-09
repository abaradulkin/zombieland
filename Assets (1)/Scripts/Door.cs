using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpened = false;
    private Transform closedDoor, openedDoor;
    private GameController gameController;
    public delegate bool OnDoorCheck(Zone targeZone);
    public OnDoorCheck DoorCheck;


    public bool IsClosed { get => !isOpened; }

    private void Start()
    {
        closedDoor = transform.Find("Closed");
        openedDoor = transform.Find("Opened");
        gameController = FindObjectOfType<GameController>();
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (gameController.IsActive && IsClosed)
            {
                Open();
            }
        }
    }

    private void Open()
    {
        if (DoorCheck(gameController.CurrentPlayer.currentZone) && gameController.CurrentPlayer.CanOpenDoor())
        {
            Debug.Log("Open door: " + this);
            gameController.StartAction();
            isOpened = true;
            closedDoor.gameObject.SetActive(false);
            openedDoor.gameObject.SetActive(true);
            gameController.FinishAction();
        }
    }
}