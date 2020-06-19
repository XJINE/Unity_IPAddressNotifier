using System.Net;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        GameObject.FindObjectOfType<IPAddressNotifier>().Notify();
    }

    public void IPAddressEventHandler1(IPAddress address)
    {
        Debug.Log("EventHandler1 : " + address);
    }

    public void IPAddressEventHandler2(IPAddress address)
    {
        Debug.Log("EventHandler2 : " + address);
    }
}