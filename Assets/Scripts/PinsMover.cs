using PathCreation;
using UnityEngine;

namespace PathCreation.Examples 
{ 
    public class PinsMover : MonoBehaviour
    {
        private Touch _touch;
        public PathCreator PathCreator;
        private Camera _camera;

        public Vector3 MoveToPoint { get; set; }

        private void Start()
       {
           _camera = Camera.main;
           _touch = new Touch();
       }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.touches[0];
                if (_touch.phase == TouchPhase.Moved)
                {
                    DragPinToPoint();
                }
            }
        }

        private void DragPinToPoint()
        {
            Ray ray = _camera.ScreenPointToRay(_touch.position);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit)) return;
            if (!hit.collider.gameObject) return;
            Vector3 worldPosition = hit.point;
            Vector3 nearestWorldPositionOnPath = PathCreator.path.GetPointAtDistance(PathCreator.path.GetClosestDistanceAlongPath(worldPosition));
            transform.position = nearestWorldPositionOnPath;
        }
    } 
}
