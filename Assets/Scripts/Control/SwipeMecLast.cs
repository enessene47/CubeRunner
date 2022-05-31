using UnityEngine;

namespace Mechanics
{
    public class SwipeMecLast : MonoBehaviour
    {
        #region Singleton
        public static SwipeMecLast instance = null;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        [Header("Swipe Variables")]
        public float clampMaxVal;
        public float lerpMult = 1;
        public float mouseDamp = 600;
        public Transform obj;

        [Header("Others")]
        private float startPosX;
        private float deltaMousePos;
      
        bool isTouchScreen;

        [HideInInspector] public Vector3 desiredPos = Vector3.zero;

        public void BaseStart()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Input.multiTouchEnabled = false;

            else
                isTouchScreen = false;
        }

        public void Swipe()
        {
            if (isTouchScreen)
                TouchControl();
            else
                MouseControl();
        }

        void MouseControl()
        {
            if (Input.GetMouseButtonDown(0))
                ResetValues();
            else if (Input.GetMouseButton(0))
                ControlOnHold();
        }

        void TouchControl()
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    ResetValues();
                    break;

                case TouchPhase.Moved:
                    ControlOnHold();
                    break;
            }
        }

        void ControlOnHold()
        {
            deltaMousePos = Input.mousePosition.x - startPosX;

            PositionMethod();
        }

        public void ResetValues() => startPosX = Input.mousePosition.x;

        void PositionMethod()
        {
            float xPos = obj.position.x;

            xPos = Mathf.Lerp(xPos, xPos + (mouseDamp * (deltaMousePos / Screen.width)), Time.deltaTime * lerpMult);

            xPos = Mathf.Clamp(xPos, -clampMaxVal, clampMaxVal);

            obj.position = new Vector3(xPos, obj.position.y, obj.position.z);

            ResetValues();
        }
    }
}

