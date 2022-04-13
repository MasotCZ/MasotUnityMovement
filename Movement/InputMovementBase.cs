﻿using Masot.Standard.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Masot.Standard.Movement
{

    public abstract class InputMovementBase : MonoBehaviour
    {
        private readonly Dictionary<MovementType, InputDelegate<EventArgsBase>> _movementTypeTranslate = new Dictionary<MovementType, InputDelegate<EventArgsBase>>();
        private InputController _inputHandler;

        public new Rigidbody2D rigidbody2D;
        public MovementWrapper2DSettings movementSettings;
        public InputMovementSettings inputSettings;
        public UnityEvent OnMove { get; } = new UnityEvent();

        protected virtual void OnEnable()
        {
            _movementTypeTranslate.Add(MovementType.Up, OnUp);
            _movementTypeTranslate.Add(MovementType.Left, OnLeft);
            _movementTypeTranslate.Add(MovementType.Down, OnDown);
            _movementTypeTranslate.Add(MovementType.Right, OnRight);

            _inputHandler = InputController.Instance;
            RegisterInput();

            Debug.Assert(movementSettings != null, $"{nameof(movementSettings)} not assigned");
            Debug.Assert(inputSettings != null, $"{nameof(inputSettings)} not assigned");
        }

        protected virtual void OnDisable()
        {
            UnregisterInput();
        }

        protected void RegisterInput()
        {
            for (int i = 0; i < inputSettings.definedInput.Length; i++)
            {
                var item = inputSettings.definedInput[i];
                _inputHandler.Register(item.input, _movementTypeTranslate[item.movementType]);
            }

            _inputHandler.Register(new InputDefine(KeyCode.Space, GetKeyType.Press), OnSpacePress);
            _inputHandler.Register(new InputDefine(KeyCode.Space, GetKeyType.Release), OnSpaceRelease);
            _inputHandler.Register(new InputDefine(KeyCode.C, GetKeyType.Hold), OnCHold);
        }

        protected void UnregisterInput()
        {
            for (int i = 0; i < inputSettings.definedInput.Length; i++)
            {
                var item = inputSettings.definedInput[i];
                _inputHandler.Remove(item.input, _movementTypeTranslate[item.movementType]);
            }

            _inputHandler.Remove(new InputDefine(KeyCode.Space, GetKeyType.Press), OnSpacePress);
            _inputHandler.Remove(new InputDefine(KeyCode.Space, GetKeyType.Release), OnSpaceRelease);
            _inputHandler.Remove(new InputDefine(KeyCode.C, GetKeyType.Hold), OnCHold);
        }

        private void OnSpacePress(EventArgs e)
        {
            OnSpacePress(movementSettings);
        }
        protected virtual void OnSpacePress(MovementWrapper2DSettings e)
        {
            e.speedMultiplier = 0.5f;
            rigidbody2D.ApplyBreakingForce(e);
            e.speedMultiplier = 1.0f;
        }

        private void OnSpaceRelease(EventArgsBase e) => OnSpaceRelease();
        protected virtual void OnSpaceRelease()
        {
            rigidbody2D.StopBreakingForce();
        }

        private void OnCHold(EventArgsBase e) => OnCHold();
        protected virtual void OnCHold()
        {
            rigidbody2D.Stop();
        }

        private void OnLeft(EventArgsBase e)
        {
            OnLeft(movementSettings);
            OnMove.Invoke();
        }

        private void OnRight(EventArgsBase e)
        {
            OnRight(movementSettings);
            OnMove.Invoke();
        }

        private void OnUp(EventArgsBase e)
        {
            OnUp(movementSettings);
            OnMove.Invoke();
        }

        private void OnDown(EventArgsBase e)
        {
            OnDown(movementSettings);
            OnMove.Invoke();
        }

        protected abstract void OnLeft(MovementWrapper2DSettings settings);
        protected abstract void OnRight(MovementWrapper2DSettings settings);
        protected abstract void OnUp(MovementWrapper2DSettings settings);
        protected abstract void OnDown(MovementWrapper2DSettings settings);
    }
}