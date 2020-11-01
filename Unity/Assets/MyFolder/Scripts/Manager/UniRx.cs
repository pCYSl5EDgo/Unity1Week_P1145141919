using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Unity1Week
{
    internal partial class Manager
    {
        private IObservable<bool> Input1, Input2;

        private void InitializeUniRx()
        {
            Input1 = this.UpdateAsObservable().Select(_ => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)).Where(_ => _).ThrottleFirst(TimeSpan.FromSeconds(1));
            Input2 = this.UpdateAsObservable().Select(_ => Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)).Where(_ => _).ThrottleFirst(TimeSpan.FromSeconds(1));
        }
    }
}