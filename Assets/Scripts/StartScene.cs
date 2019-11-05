using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.Instance != null)
        {
            NetworkManager.Instance.InstantiatePlayer();
        }
        NetworkManager.Instance.Networker.playerDisconnected += Disco;
    }

    private void Disco(NetworkingPlayer player, NetWorker sender)
    {
        // single object 
        //	if (networkObject.Owner == player) 
        //	networkObject.Destroy(); 

        // all owned objects 
        List<NetworkObject> toDelete = new List<NetworkObject>();
        foreach (var no in sender.NetworkObjectList)
        {
            if (no.Owner == player)
            {
                //Found him 
                toDelete.Add(no);
            }
        }

        if (toDelete.Count > 0)
        {
            for (int i = toDelete.Count - 1; i >= 0; i--)
            {
                sender.NetworkObjectList.Remove(toDelete[i]);
                toDelete[i].Destroy();
            }
        }
    }
}
