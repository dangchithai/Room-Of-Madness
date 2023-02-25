using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerRoom : Collidable
{
    [SerializeField] private GameObject[] monsters;
    [SerializeField] Transform[] spawnMonsterLocations;
    [SerializeField] private GameObject[] blockObjs;
    [SerializeField] private GameObject[] linkDoors;

    bool isTriggerCreationRoom;

    [HideInInspector]public int NumberOfMonsterAlive;

    enum RoomMode
    {
        LegendaryReward,
        Reward,
        Monster,
        Puzzle
    }

    private RoomMode currentRoomMode;

    protected override void Start()
    {
        base.Start();

        NumberOfMonsterAlive = monsters.Length;
    }

    protected override void Update()
    {
        base.Update();

        if (isTriggerCreationRoom && currentRoomMode == RoomMode.Monster)
        {
            if (NumberOfMonsterAlive <= 0)
            {
                OnFinishTriggerRoom();
            }
        }
    }

    protected override void OnCollide(Collider2D hit)
    {
        if (isTriggerCreationRoom)
        {
            return;
        }

        if(hit.gameObject.tag == "Player")
        {
            float valueRand = Random.value;
            if (valueRand > 0.999f) // 0.001%
            {
                currentRoomMode = RoomMode.LegendaryReward;
            }
            else if (valueRand > 0.95f) // 5%
            {
                currentRoomMode = RoomMode.Reward;
            }
            else if(valueRand > 0.65f) // 30%
            {
                currentRoomMode = RoomMode.Puzzle;
            }
            else
            {
                currentRoomMode = RoomMode.Monster;
            }

            Debug.Log("Current Room Mode is : " + currentRoomMode);

            switch (currentRoomMode)
            {
                case RoomMode.Monster:
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        Instantiate(monsters[i], spawnMonsterLocations[i]);
                    }
                    break;
            }

            isTriggerCreationRoom = true;
            GameManager.Instance.ActiveRoom = this;
        }
    }

    public void OnFinishTriggerRoom()
    {
        foreach (var blockObj in blockObjs)
        {
            blockObj.SetActive(false);
        }

        foreach (var linkDoor in linkDoors)
        {
            linkDoor.SetActive(true);
        }
    }
}
