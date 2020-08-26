using System.Net;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        GameObject.FindObjectOfType<IPAddressNotifier>().Notify();
    }

    public void IPAddressEventHandler1(IPAddress ipAddress)
    {
        Debug.Log("EventHandler1 : " + ipAddress);
    }

    public void IPAddressEventHandler2(IPAddress ipAddress)
    {
        Debug.Log("EventHandler2 : " + ipAddress);
    }
}