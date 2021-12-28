using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sneaker.Movement
{
   public class _CustomerMovement : MonoBehaviour
    {
        [HideInInspector]public NavMeshAgent agent;
        [HideInInspector]public float Rotate;
        
        public Sneaker.Core._LevelManager levelManager;
        private float turnSmoothVelocity;
        public Transform target;
        public Transform end;
        
        
        public Animator anime;
        public float rotationSmooth = 0.01f;
        public float lookRotation = 90;


        public bool isRechedStation = false;
        public bool tradeIsOver = false;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();            
        }

        
        void Update()
        {
            if (target == null && !GetComponent<Sneaker.Control._CustomerControl>().isTradingGoingOn)
                targetSet();
            move();
        }

        void move()
        {
            anime.SetFloat("speed", Mathf.Abs(agent.velocity.magnitude));
            if (agent.velocity.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(agent.velocity.x, agent.velocity.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }

            if (!tradeIsOver && !GetComponent<Sneaker.Control._CustomerControl>().isTradingGoingOn)
            {
                agent.SetDestination(target.position);                
            }

            if (tradeIsOver)
            {
                agent.SetDestination(end.position);
/*                if (target != null)
                {
                    target.GetComponent<dottedCircle>().occupied = false;.
                    target = null;
                }    */            
            }

        }

        void targetSet()
        {
            if (levelManager.StationPosition.Count >= 0)
            {
                for(int i=0;i<= levelManager.StationPosition.Count;)
                {
                    if (!levelManager.StationPosition[i].GetComponent<dottedCircle>().occupied)
                    {
                        target = levelManager.StationPosition[i].transform;
                        target.GetComponent<dottedCircle>().occupied = true;
                        Rotate = target.GetComponent<dottedCircle>().Rotate;
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}
