using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour {

    [SerializeField] List<Room> connectedRooms;
    [SerializeField] int price;

    private Animator animator;
    private bool IsOpen = false;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    public void Open(GameObject player) {
        if (player.GetComponentInParent<PlayerMoney>().currentMoney >= price) {
            if (IsOpen)
                return;

            IsOpen = true;
            animator.SetBool("IsOpen", true);
            player.GetComponentInParent<PlayerMoney>().LooseMoney(price);
            foreach (Room room in connectedRooms) {
                room.ActivateRoom();
            }
        }
        else {
            Debug.Log("Not enough money");
        }
    }

    public void Close() {
        animator.SetBool("IsOpen", false);
        IsOpen = false;
    }
}
