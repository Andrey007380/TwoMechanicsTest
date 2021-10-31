using System;
using PathCreation;
using UnityEditor;
using UnityEngine;

namespace PathCreation.Examples 
{ 
    public class PinsMover : MonoBehaviour
    {
        private Touch _touch;
        public PathCreator pathCreator;
        private Camera _camera;
        

        private void Start()
       {
           _camera = Camera.main;
           _touch = new Touch();
       }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            TouchDrag();
        }

        private void TouchDrag()
        {
            _touch = Input.touches[0];
            if (_touch.phase != TouchPhase.Moved) return;
            Ray ray = _camera.ScreenPointToRay(_touch.position);
            RaycastHit hit;
            
            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.collider.gameObject == gameObject)
            {
                Vector3 worldPosition = hit.point;
                Vector3 nearestWorldPositionOnPath = pathCreator.path.GetPointAtDistance(pathCreator.path.GetClosestDistanceAlongPath(worldPosition));
                transform.position = nearestWorldPositionOnPath;
            }
        }
        
        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.WorldToScreenPoint(gameObject.transform.position).z);
            Vector3 curPosition =_camera.ScreenToWorldPoint(curScreenPoint);
            
            Vector3 nearestWorldPositionOnPath = pathCreator.path.GetPointAtDistance(pathCreator.path.GetClosestDistanceAlongPath(curPosition));
            transform.position = nearestWorldPositionOnPath;
        }
    } 
}
