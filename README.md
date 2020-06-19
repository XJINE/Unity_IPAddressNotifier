# Unity_IPAddressNotifier

<img src="https://github.com/XJINE/Unity_IPAddressNotifier/blob/master/Screenshot.png" width="100%" height="auto" />

``IPAddressNotifier`` provides simple Observer pattern logic to notify local IPAddresses.

## Import to Your Project

You can import this asset from UnityPackage.

- [IPAddressNotifier.unitypackage](https://github.com/XJINE/Unity_IPAddressNotifier/blob/master/IPAddressNotifier.unitypackage)

## How to Use

Add some observers from Inspector and call the ``Notify`` to notify IPAddresses.

Some filter settings are used to ignore the notify. 

Ex. when the ``FilterIPv4`` shows true, these handlers are not received IPv4 address notify.

Ex. when the ``FilterStartsWith`` shows ``10.21``, some address like ``10.21.0.1``, ``10.21.0.2`` and any others are filtered.

```csharp
public void IPAddressEventHandler(IPAddress address)
{
    Debug.Log(address);
}
```

Observer's handler gets the address as args.
