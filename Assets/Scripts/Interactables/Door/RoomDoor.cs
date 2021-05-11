using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour {

    [SerializeField] List<Room> connectedRooms;

    private Animator animator;
    private bool IsOpen = false;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public  void Open(GameObject player) {
        animator.SetBool("IsOpen", true);

        foreach (Room room in connectedRooms) {
            room.ActivateRoom();
        }
    }

    public void Cose() {
        animator.SetBool("IsOpen", false);
    }
    //private void OpenDoor() {
    //    countDown -= Time.deltaTime;
    //    if (countDown <= 0) {
    //        if (doorAngle < 150) {
    //            doorAngle += 10;
    //            door.transform.rotation = Quaternion.Euler(0, doorAngle, 0);
    //            countDown = openSpeed;
    //        }

    //    }
    //}
}
