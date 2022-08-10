using System;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
   private Action _actionOnTimer;
   private float _timer;

   public void SetTimer(float timer, Action actionOnTimer)
   {
      _timer = timer;
      _actionOnTimer = actionOnTimer;
   }

   private void Update()
   {
      if (_timer > 0)
      {
         _timer -= Time.deltaTime;

         if (IsTimerComplete()) _actionOnTimer();
      }
   }

   public bool IsTimerComplete() => _timer <= 0;
}
