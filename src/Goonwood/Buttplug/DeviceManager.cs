using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buttplug.Client;
using Buttplug.Core;
using Buttplug.Core.Messages;
using UnityEngine;
using ButtplugWebsocketConnector = Buttplug.Client.ButtplugWebsocketConnector;

namespace Goonwood.Buttplug;

public class DeviceManager
{
    /// <summary>
    /// A list of connected devices that have a vibrate output
    /// </summary>
    public List<ButtplugClientDevice> ConnectedDevices { get; set; }

    private ButtplugClient ButtplugClient { get; set; }
    private string ServerUri { get; set; }

    public DeviceManager(string clientName, string serverUri)
    {
        ConnectedDevices = [];
        ButtplugClient = new ButtplugClient(clientName);
        ServerUri = serverUri;

        Goonwood.Log.LogInfo("BP client created for " + clientName);
        ButtplugClient.DeviceAdded += HandleDeviceAdded;
        ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
    }

    public async void ConnectDevices()
    {
        if (ButtplugClient.Connected) return;

        try
        {
            Goonwood.Log.LogInfo($"Attempting to connect to Intiface server at {ServerUri}");
            await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(ServerUri)));
            Goonwood.Log.LogInfo("Connection successful. Beginning scan for devices");
            await ButtplugClient.StartScanningAsync();
        }
        catch (ButtplugException exception)
        {
            Goonwood.Log.LogError(
                $"Attempt to connect to devices failed. Ensure Intiface is running and attempt to reconnect from the 'Devices' section in the mod's in-game settings.");
            Goonwood.Log.LogDebug($"ButtplugIO error occured while connecting devices: {exception}");
        }
    }

    public async void Reconnect(string serverUri)
    {
        Goonwood.Log.LogInfo("Disconnecting from WebSocket connector");
        await ButtplugClient.DisconnectAsync();
        Goonwood.Log.LogInfo("Connecting to new Buttplug WebSocket");
        await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(serverUri)));
        Goonwood.Log.LogInfo("Connection successful.");
    }

    public async void Disconnect()
    {
        StopConnectedDevices();
        ConnectedDevices.Clear();
        await ButtplugClient.DisconnectAsync();
    }

    public async void VibrateConnectedDevices(float intensity)
    {
        var percentage = Mathf.Clamp(intensity, 0f, 1.0f);

        foreach (var device in ConnectedDevices)
        {
            await device.RunOutputAsync(DeviceOutput.Vibrate.Percent(percentage));
        }
    }

    public async void VibrateConnectedDevicesWithDuration(float intensity, float time)
    {
        var percentage = Mathf.Clamp(intensity, 0f, 1.0f);

        foreach (var device in ConnectedDevices)
        {
            await device.RunOutputAsync(DeviceOutput.Vibrate.Percent(percentage));
            await Task.Delay((int)(time * 1000f));
            await device.RunOutputAsync(DeviceOutput.Vibrate.Percent(0f));
        }
    }

    public async void StopConnectedDevices()
    {
        Goonwood.Log.LogDebug($"Stopping {ConnectedDevices.Count} connected devices");

        foreach (var device in ConnectedDevices)
        {

            // NOTE: StopAsync is currently broken on v5.0.0
            await device.RunOutputAsync(DeviceOutput.Vibrate.Percent(0f));
        }
    }

    public bool IsConnected() => ButtplugClient.Connected;

    private void HandleDeviceAdded(object sender, DeviceAddedEventArgs args)
    {
        if (!IsVibratableDevice(args.Device))
        {
            Goonwood.Log.LogInfo($"{args.Device.Name} was detected but ignored due to it not being vibratable.");
            return;
        }

        Goonwood.Log.LogInfo($"{args.Device.Name} connected to client {ButtplugClient.Name}");
        ConnectedDevices.Add(args.Device);
    }

    private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
    {
        if (!IsVibratableDevice(args.Device))
        {
            return;
        }

        Goonwood.Log.LogInfo($"{args.Device.Name} disconnected from client {ButtplugClient.Name}");
        ConnectedDevices.Remove(args.Device);
    }

    private static bool IsVibratableDevice(ButtplugClientDevice device) => device.HasOutput(OutputType.Vibrate);
}
