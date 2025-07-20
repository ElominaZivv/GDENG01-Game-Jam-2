using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardRandomizer : MonoBehaviour
{

    //[SerializeField] GameObject Deck;
    //[SerializeField] Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShuffleCardPositions();
        }
    }
    private void ShuffleCardPositions()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform cardA = transform.GetChild(i);
            int randomIndex = Random.Range(0, childCount);
            Transform cardB = transform.GetChild(randomIndex);

            Vector3 tempPos = cardA.position;
            cardA.position = cardB.position;
            cardB.position = tempPos;
        }

        Debug.Log("Cards have been Shuffled");
    }
}
