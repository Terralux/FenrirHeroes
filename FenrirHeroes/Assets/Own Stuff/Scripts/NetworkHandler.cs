using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class NetworkHandler : MonoBehaviour
{

    public List<string> playerID = new List<string>();


    // Use this for initialization
    void Awake()
    {

        Toolbox.RegisterComponent<NetworkHandler>(this);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < playerID.Count; i++)
            {
                Debug.Log(playerID[i]);
            }

        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Is Master Client: " + PhotonNetwork.isMasterClient);
        }

    }

   
	public void givePlayerID(string deviceID)
    {
      playerID.Add(deviceID);
    }


}
