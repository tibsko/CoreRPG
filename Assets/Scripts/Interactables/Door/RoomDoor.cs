using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour {

    [SerializeField] List<Room> connectedRooms;

    private Animator animator;
    private bool IsOpen = false;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    public void Open(GameObject player) {
        if (IsOpen)
            return;

        IsOpen = true;
        animator.SetBool("IsOpen", true);

        foreach (Room room in connectedRooms) {
            room.ActivateRoom();
        }
    }

    public void Close() {
        animator.SetBool("IsOpen", false);
        IsOpen = false;
    }
}
