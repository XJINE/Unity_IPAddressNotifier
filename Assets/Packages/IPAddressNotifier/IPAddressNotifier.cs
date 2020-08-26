using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class IPAddressNotifier : MonoBehaviour
{
    #region Class

    [Serializable] public class IPAddressEvent : UnityEvent<IPAddress> { }

    [Serializable] public class IPAddressObserver
    {
        public bool   filterIPv4;
        public bool   filterIPv6;
        public string filterStartsWith;
        public string filterEndsWith;
        public string filterRegex;

        public IPAddressEvent handler;

        public bool Filter(IPAddress ipAddress)
        {
            // NOTE:
            // Return 'true' when the ipAddress is filtered.

            // NOTE:
            // Following codes consider the "address.IsIPv4MappedToIPv6" as IPv4.

            if (ipAddress.IsIPv6LinkLocal
             || ipAddress.IsIPv6Multicast
             || ipAddress.IsIPv6SiteLocal
             || ipAddress.IsIPv6Teredo)
            {
                if (this.filterIPv6)
                {
                    return true;
                }
            }
            else if (this.filterIPv4)
            {
                return true;
            }

            string address = ipAddress.ToString();

            if (!string.IsNullOrWhiteSpace(this.filterStartsWith) && address.StartsWith(this.filterStartsWith))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(this.filterEndsWith) && address.EndsWith(this.filterEndsWith))
            {
                return true;
            }

            if (!string.IsNullOrWhiteSpace(this.filterRegex) && Regex.IsMatch(address, this.filterRegex))
            {
                return true;
            }

            return false;
        }
    }

    #endregion Class

    #region Field

    public bool notifyOnStart = true;

    public List<IPAddressObserver> observers;

    #endregion Field

    #region Method

    public virtual void Start()
    {
        if (this.notifyOnStart)
        {
            Notify();
        }
    }

    public virtual void Notify()
    {
        Notify(this.observers);
    }

    public static void Notify(IList<IPAddressObserver> observers)
    {
        var addresses = GetLocalAddresses();

        foreach (IPAddress address in addresses)
        {
            foreach (IPAddressObserver observer in observers)
            {
                if (observer.Filter(address))
                {
                    continue;
                }

                observer.handler.Invoke(address);
            }
        }
    }

    public static IPAddress[] GetLocalAddresses()
    {
        // NOTE:
        // Addresses are need not to be cached.
        // In General, these are not required in real-time process.
        // But it sometimes needs to be updated in runtime.

        return Dns.GetHostAddresses(Dns.GetHostName());
    }

    #endregion Method
}