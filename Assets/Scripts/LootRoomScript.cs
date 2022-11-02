using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootRoomScript : MonoBehaviour
{
    [SerializeField] GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            GameManager.Instance.EnterLootRoom();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            GameManager.Instance.ExitLootRoom();
        }
    }
}
