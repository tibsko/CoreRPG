using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    public float Time { get; private set; }
    public float TargetTime { get; private set; }


    public bool Done { get => Time >= TargetTime; }

    public Timer(float targetTimer) {
        TargetTime = targetTimer;
        Time = 0;
    }

    public void Tick(float deltaTime) {
        Time += deltaTime;
    }

}
