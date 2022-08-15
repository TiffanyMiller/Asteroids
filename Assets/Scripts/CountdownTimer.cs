using System;
using UnityEngine;

public class CountdownTimer
{
   private Action _actionOnTimer;
   private float _timer;

   public void Set(float timer, Action actionOnTimer)
   {
      _timer = timer;
      _actionOnTimer = actionOnTimer;
   }

   public void Run()
   {
      if (_timer > 0)
      {
         _timer -= Time.deltaTime;

         if (IsTimerComplete()) _actionOnTimer?.Invoke();
      }
   }

   public bool IsTimerComplete() => _timer <= 0;
}
