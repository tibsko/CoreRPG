using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
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

    public  void Open(GameObject player) {
        animator.SetBool("IsOpen", true);
        GameObject[] roomsActive = GameObject.FindGameObjectsWithTag("Room");

        //Disable other rooms (and spawners)
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
