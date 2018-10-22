using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;

public class ServerSocket : MonoBehaviour {

    

	// Use this for initialization
	void Start ()
    {
        int i = Assets.DataManager.instance().getI();
        Assets.DataManager.instance().setI(20);
        Debug.Log(i);

        using (var ws = new WebSocket(Const.SERVER_IP+":"+Const.SERVER_PORT))
        {
            /*ws.OnMessage += (sender, e) =>
                Console.WriteLine("Laputa says: " + e.Data);

            ws.Connect();
            ws.Send("BALUS");
            Console.ReadKey(true);*/
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void loadChank()
    {

    }
}
