using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.AI;


class ZombieControllerSystem : ComponentSystem
{
    // Start is called before the first frame update
   struct Components {
        private Transform target;
        private NavMeshAgent agent;
        private Animator animator;
        private WalkerAttack enemyAttack;
        private DoorDetector doorDetector;
    }
    protected override void OnUpdate() {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity entity = entityManager.CreateEntity();
    }


}   
