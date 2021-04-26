using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    public float Time { get; private set; }
    public float TargetTime { get; set; }
    public bool Done { get => Time >= TargetTime; }

    public Timer(float targetTimer) {
        TargetTime = targetTimer;
        Time = 0;
    }

    public void Tick(float deltaTime) {
        Time += deltaTime;
    }

    public void Reset() {
        Time = 0;
    }
}

public class InvTimer {
    public float Time { get; private set; }
    public float StartTime { get; private set; }
    public bool Done { get => Time <= 0; }

    public InvTimer(float startTimer) {
        StartTime = startTimer;
        Time = StartTime;
    }

    public void Tick(float deltaTime) {
        Time -= deltaTime;
    }

    public void Reset() {
        Time = StartTime;
    }
}