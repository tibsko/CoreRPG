using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoorInteractable : Interactable
{
    [SerializeField] GameObject door;
    [SerializeField] float openSpeed;

    private Animator animator;
    private float countDown;
    private float doorAngle;
    private bool isOpenning=false;
    private List<Room> rooms;
    
    // Start is called before the first frame update
    void Start()
    {
        doorAngle = door.transform.rotation.y;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void Interact(GameObject player) {
        base.Interact(player);
        animator.SetBool("isOpenning", true);
        GameObject[] roomsActive = GameObject.FindGameObjectsWithTag("Room");
        foreach(GameObject room in roomsActive) {
            if (room.activeInHierarchy) {
                room.SetActive(false);
                room.GetComponent<Room>().ActiveRoomSpawners(false);
            }
        }

        foreach (Room room in rooms) {
            room.gameObject.SetActive(true);
            room.ActiveRoomSpawners(true);
        }

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
