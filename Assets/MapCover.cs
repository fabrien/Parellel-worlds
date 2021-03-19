using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCover : MonoBehaviour
{
    [SerializeField]
    private int keyChange = 3;

    // 0 is left map, 1 is right map
    private int currentPos = 1;

    private int keyCount = 0;

    public GameObject mapCoverSquare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            keyCount++;
        }

        if (keyCount == keyChange)
        {
            if(currentPos == 0)
            {
                mapCoverSquare.transform.position = new Vector2(7.4081f, 0);
                currentPos = 1;
            }
            else if(currentPos == 1)
            {
                mapCoverSquare.transform.position = new Vector2(-7.15f, 0);
                currentPos = 0;
            }

            keyCount = 0;
        }
    }
}
