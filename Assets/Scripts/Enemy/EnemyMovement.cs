using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyMovement : CharacterMovement
    {
        public Transform EnemyContainerTransform;
        public Rigidbody2D EnemyRigidBody;
        public Animator EnemyAnimator;
        [SerializeField]
        public float Speed;

        [Tooltip("The amount of time the enemy remains stopped at a waypoint.")]
        public float StopTime;

        public bool OnlyX = true;

        public Transform WaypointsContainer;

        private List<Vector3> _waypoints = new List<Vector3>();
        private int _waypointsCount;
        private int _waypointIndex;

        public bool Moving;
        private bool _stopped;

        private Vector3 _spawnPoint;

        public override Rigidbody2D CharacterRigidBody
        {
            get
            {
                return EnemyRigidBody;
            }
        }

        void OnEnable()
        {
            _waypointIndex = 0;
        }

        void Start()
        {
            _spawnPoint = transform.position;
            var childWaypointCount = WaypointsContainer.childCount;
            for (int i = 0; i<childWaypointCount; i++)
            {
                _waypoints.Add(WaypointsContainer.GetChild(i).position);
            }

            _waypointsCount = _waypoints.Count();
        }

        void Update()
        {
            if (!Moving && !_stopped)
            {
                StartCoroutine(MoveAndStop());
            }
        }

        public void Reset()
        {
            EnemyContainerTransform.position = _spawnPoint;
            _waypointIndex = 0;
        }

        private IEnumerator MoveAndStop()
        {
            
            var newWaypoint = _waypoints[_waypointIndex];
            _waypointIndex = (_waypointIndex + 1) % _waypointsCount;
            StartCoroutine(GoToWaypoint(newWaypoint));

            while (Moving)
                yield return null;

            var startTime = Time.time;

            while (Time.time - startTime < StopTime)
            {
                _stopped = true;
                EnemyAnimator.SetTrigger("Stop");
                yield return null;
            }


            _stopped = false;
        }


        private IEnumerator GoToWaypoint(Vector3 waypoint)
        {
            Vector3 startingPosition = transform.position;
            Vector3 direction = waypoint - startingPosition;
            if (OnlyX)
            {
                direction.y = 0;
                waypoint.y = 0;
            }
            direction = direction.normalized;
            SpriteRenderer temp = GetComponentInParent<SpriteRenderer>();
            if (direction.x > 0)
            {
                temp.flipX = true;
            } else
            {
                temp.flipX = false;
            }

            float startingTime = Time.time;

            var currentPosition = transform.position;
            while ((currentPosition - waypoint).sqrMagnitude >0.1f)
            {
                EnemyRigidBody.velocity = direction * Speed;
                Moving = true;
                EnemyAnimator.SetTrigger("Move");

                yield return new WaitForFixedUpdate();

                currentPosition = transform.position;
                if (OnlyX)
                    currentPosition.y = 0;

            }
            Moving = false;

            EnemyRigidBody.velocity = Vector2.zero;
        }


    }
}
