using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    void Update()
    {
        if (player == null)
        {
            Debug.Log("<color=red>Error:</color> Add player please");
            return;
        }
        transform.position = player.position;
    }
}
