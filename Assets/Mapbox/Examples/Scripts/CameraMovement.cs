namespace Mapbox.Examples
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Mapbox.Unity.Map;
    using UnityEngine.UI;

    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        float _panSpeed = 0;

        [SerializeField]
        float _zoomSpeed = 20f;

        [SerializeField]
        Camera _referenceCamera;

        [SerializeField]
        GameObject quitMenuPanel;

        [SerializeField]
        GameObject player;

        [SerializeField]
        GameObject CameraPositionOrigin;

        [SerializeField]
        Text zoomLevelCheck;

        Vector2 firstFingerPosition;
        Vector2 lastFingerPosition;
        float angle;
        float swipeDistanceX;
        float swipeDistanceY;
        float SWIPE_DISTANCE_X_CONST = 60;
        float SWIPE_DISTANCE_Y_CONST = 150;

        void HandleTouch()
        {
            float zoomFactor = 0.0f;

            //pinch to zoom. 
            switch (Input.touchCount)
            {
                case 1:
                    {
                        Touch touch = Input.GetTouch(0);

                        // Raycast
                        if (touch.phase == TouchPhase.Began)
                        {
                            firstFingerPosition = touch.position;
                            lastFingerPosition = touch.position;

                            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                            RaycastHit hitInfo;
                            int layer = 1 << LayerMask.NameToLayer("Marker");
                            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
                            {
                                BtnEvent.instance.OpenMarkerInfoPanel();
                            }
                        }

                        // Swipe Control
                        if (touch.phase == TouchPhase.Moved)
                        {
                            lastFingerPosition = touch.position;
                            swipeDistanceX = Mathf.Abs((lastFingerPosition.x - firstFingerPosition.x));
                            swipeDistanceY = Mathf.Abs((lastFingerPosition.y - firstFingerPosition.y));

                            angle = Mathf.Atan2((lastFingerPosition.x - firstFingerPosition.x), (lastFingerPosition.y - firstFingerPosition.y)) * 57.2957795f;

                            if (angle <= -50 && angle >= -110 && swipeDistanceX > SWIPE_DISTANCE_X_CONST)
                            {
                                transform.RotateAround(player.transform.position, player.transform.up, -10);
                                Debug.Log("left swipe...");
                            }
                            else if (angle >= 50 && angle <= 110 && swipeDistanceX > SWIPE_DISTANCE_X_CONST)
                            {
                                transform.RotateAround(player.transform.position, player.transform.up, 10);
                                Debug.Log("right swipe...");
                            }
                        }

                        if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                        {
                            if (transform.localPosition.x != CameraPositionOrigin.transform.localPosition.x && transform.localPosition.y != CameraPositionOrigin.transform.localPosition.y)
                            {
                                transform.position = CameraPositionOrigin.transform.position;
                                transform.LookAt(player.transform);
                            }
                        }
                    }
                    break;

                case 2:
                    {
                        // Store both touches.
                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        // Find the position in the previous frame of each touch.
                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        // Find the magnitude of the vector (the distance) between the touches in each frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        // Find the difference in the distances between each frame.
                        zoomFactor = 0.05f * (touchDeltaMag - prevTouchDeltaMag);
                    }
                    if ((transform.localPosition.z >= 3200 && zoomFactor > 0) || (transform.localPosition.z <= -180 && zoomFactor < 0)) return;
                    ZoomMapUsingTouchOrMouse(zoomFactor);
                    break;
                default:
                    break;
            }
        }

        void ZoomMapUsingTouchOrMouse(float zoomFactor)
        {
            PlayerRigPos.instance.isChangeScale = true;
            BtnEvent.instance.isFocusing = false;
            var y = zoomFactor * _zoomSpeed;
            transform.localPosition += (Vector3.forward * y);
            transform.localPosition = new Vector3(0, 0, Mathf.Clamp(transform.localPosition.z, -180, 3200));
            CameraPositionOrigin.transform.localPosition = transform.localPosition;
        }

        void Awake()
        {
            if (_referenceCamera == null)
            {
                _referenceCamera = GetComponent<Camera>();
                if (_referenceCamera == null)
                {
                    throw new System.Exception("You must have a reference camera assigned!");
                }
            }

            if (_map == null)
            {
                _map = FindObjectOfType<AbstractMap>();
                if (_map == null)
                {
                    throw new System.Exception("You must have a reference map assigned!");
                }
            }
        }

        void LateUpdate()
        {
            if (quitMenuPanel.activeSelf) return;
            if (Input.touchSupported && Input.touchCount > 0)
            {
                HandleTouch();
            }
            zoomLevelCheck.text = transform.localPosition.z.ToString();
        }
    }
}