using design_patterns.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace design_patterns
{
    /// <summary>
    /// Command is a behavioral design pattern that turns a request into a stand-alone object that contains all information about the request. 
    /// This transformation lets you parameterize methods with different requests, delay or queue a request’s execution, 
    /// and support undoable operations.
    /// </summary>
    class CommandPattern : DesignPattern
    {
        public override string PatternName => "Command";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var device = new Device();
            var activateCommand = new SwitchOnRemoteControlCommand();
            var switchOnCommand = new SwitchOnDeviceCommand(device);
            var changeVolumeCommand = new ChangeVolumeCommand(device, 50);

            var remoteControl = new RemoteControl(activateCommand, switchOnCommand, changeVolumeCommand);

            remoteControl.ClickRemoteButton1();
            remoteControl.ClickRemoteButton2();
        }
    }

    interface ICommand
    {
        /// <summary>
        /// Since the command execution method doesn’t have any parameters, how would we pass the request details to the receiver? 
        /// It turns out the command should be either pre-configured with this data, or capable of getting it on its own.
        /// </summary>
        void Execute();
    }

    class SwitchOnRemoteControlCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine($"Simple command to switch on remote control");
        }
    }

    class SwitchOnDeviceCommand : ICommand
    {
        private Device _device;
        public SwitchOnDeviceCommand(Device device)
        {
            _device = device;
        }
        public void Execute()
        {
            _device.InitializeConnection();
            _device.SwitchOn();
        }
    }

    class ChangeVolumeCommand : ICommand
    {
        private int _volume;
        private Device _device;
        public ChangeVolumeCommand(Device device, int volume)
        {
            _device = device;
            _volume = volume;
        }
        public void Execute()
        {
            _device.InitializeConnection();
            _device.ChangeVolume(_volume);
        }
    }

    class RemoteControl
    {
        ICommand _switchOnDeviceCommand;
        ICommand _switchOnRemoteControlCommand;
        ICommand _changeVolumeCommand;

        public RemoteControl(ICommand activate, ICommand switchOn, ICommand changeVolume)
        {
            _switchOnRemoteControlCommand = activate;
            _switchOnDeviceCommand = switchOn;
            _changeVolumeCommand = changeVolume;
        }

        public void ClickRemoteButton1()
        {
            _switchOnRemoteControlCommand.Execute();
            _switchOnDeviceCommand.Execute();
        }

        public void ClickRemoteButton2()
        {
            _switchOnRemoteControlCommand.Execute();
            _changeVolumeCommand.Execute();
        }
    }

    class Device
    {
        public void InitializeConnection()
        {
            Console.WriteLine($"Connected: {DateTime.Now.ToLongTimeString()}");
        }
        public void SwitchOn()
        {
            Console.WriteLine($"Switched on: {DateTime.Now.ToLongTimeString()}");
        }
        public void SwitchOff()
        {
            Console.WriteLine($"Switched off: {DateTime.Now.ToLongTimeString()}");
        }
        public void ChangeVolume(int value)
        {
            Console.WriteLine($"Volume changed to value: {value}");
        }
        public void NextChannel()
        {
            Console.WriteLine($"Moved to next chanel: {DateTime.Now.ToLongTimeString()}");
        }
        public void PreviousChannel()
        {
            Console.WriteLine($"Moved to previous chanel: {DateTime.Now.ToLongTimeString()}");
        }
    }
}
