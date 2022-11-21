using System;
using UnityEngine;

namespace Script.InputSpace
{
    public class KeyboardInput : MonoBehaviour, IInput
    {
        public event Action<float> OnLeftEvent;
        public event Action<float> OnRightEvent;
        public event Action<float> OnUpEvent;
        public event Action<float> OnDownEvent;
        public event Action OnSwitchEvent;
        
        public void Update()
        {
            CheckHorizontal();
            CheckVertical();
            CheckSwitch();
            
        }

        private void CheckSwitch()
        {
            var value = Input.GetButtonUp("Switch");
            if (value)
            {
                OnSwitchEvent?.Invoke();
            }
        }

        private void CheckVertical()
        {
            var verticalValue = Input.GetAxis("Vertical");
            if (verticalValue > 0)
            {
                verticalValue = 1;
                OnUpEvent?.Invoke(verticalValue);
            }
            else if (verticalValue < 0)
            {
                verticalValue = 1;
                OnDownEvent?.Invoke(Math.Abs(verticalValue));
            }
        }

        private void CheckHorizontal()
        {
            var horizontalValue = Input.GetAxis("Horizontal");
            if (horizontalValue > 0)
            {
                horizontalValue = 1;
                OnRightEvent?.Invoke(horizontalValue);
            }
            else if (horizontalValue < 0)
            {
                horizontalValue = 1;
                OnLeftEvent?.Invoke(Math.Abs(horizontalValue));
            }
        }
    }
}