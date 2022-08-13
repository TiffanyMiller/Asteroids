using System;
using UnityEngine;

public static class CountdownTimer
{
   private static Action _actionOnTimer;
   private static float _timer;

   public static void Set(float timer, Action actionOnTimer)
   {
      _timer = timer;
      _actionOnTimer = actionOnTimer;
   }

   public static void Run()
   {
      if (_timer > 0)
      {
         _timer -= Time.deltaTime;

         if (IsTimerComplete()) _actionOnTimer();
      }
   }

   public static bool IsTimerComplete() => _timer <= 0;
}
