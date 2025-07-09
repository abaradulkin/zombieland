using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
    [Header("Zone attributes")]
    public Door door;
    [SerializeField] private GameObject[] right, left, top, bottom;
    [Space]
    public EventVector3 OnClickEnvironment;
       
    private GameObject[] neighbours;
    private GameController gameController;

    

    void Start()
    {
        neighbours = new List<GameObject>().Concat(right).Concat(left).Concat(top).Concat(bottom).ToArray();
        gameController = FindObjectOfType<GameController>();
        if (door)
        {
            door.DoorCheck = IsNext;
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (gameController.IsActive)
            {
                StartCoroutine(Move());
            }
        }
    }

    private bool IsNext(Zone target)
    {
        foreach (GameObject zone in neighbours) 
        {
            if (target.gameObject == zone) return true;
        }
        return false;
    }

    private IEnumerator Move()
    {
        Player player = gameController.GetCurrentPlayer();

        if (IsNext(player.currentZone))
        {
            if (door && door.IsClosed)
            {
                Debug.Log("Door closed: " + door);
            }
            else
            {
                Debug.Log("Move to: " + this);
                gameController.StartAction();
                //OnClickEnvironment.Invoke(GetRandomPositionInside());
                player.GetComponentInChildren<NavMeshAgent>().destination = GetRandomPositionInside();
                yield return new WaitForSeconds(2);
                gameController.FinishAction();
                player.currentZone = this;
            }
        }
    }

    private Vector3 GetRandomPositionInside()
    {
        return transform.position;
        //Vector3 randomDirection = Random.insideUnitSphere * 1;
        //randomDirection += transform.position;
        //NavMeshHit hit;
        //Vector3 finalPosition = Vector3.zero;
        //if (NavMesh.SamplePosition(randomDirection, out hit, 1, 1))
        //{
        //    finalPosition = hit.position;
        //}
        //Debug.Log(finalPosition);
        //return finalPosition;
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }