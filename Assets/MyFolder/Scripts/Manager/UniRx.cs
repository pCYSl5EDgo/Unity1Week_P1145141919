using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;
using UniRx;
using UniRx.Triggers;

namespace Unity1Week
{
    partial class Manager
    {
        IObservable<bool> Input1, Input2;
        private void InitializeUniRx()
        {
            Input1 = this.UpdateAsObservable().Select(_ => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)).Where(_ => _).ThrottleFirst(TimeSpan.FromSeconds(1));
            Input2 = this.UpdateAsObservable().Select(_ => Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)).Where(_ => _).ThrottleFirst(TimeSpan.FromSeconds(1));
        }
    }
}