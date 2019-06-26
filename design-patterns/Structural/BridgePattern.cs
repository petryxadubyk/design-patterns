using System;
using System.Collections.Generic;
using System.Text;

namespace design_patterns
{
    /*
    The GoF book  introduces the terms Abstraction and Implementation as part of the Bridge definition. 
    In my opinion, the terms sound too academic and make the pattern seem more complicated than it really is. 

    Abstraction (also called interface) is a high-level control layer for some entity. 
    This layer isn’t supposed to do any real work on its own. 
    It should delegate the work to the implementation layer (also called platform).

    Note that we’re not talking about interfaces or abstract classes from your programming language. These aren’t the same things.

    Example:
    When talking about real applications, the abstraction can be represented by a graphical user interface (GUI), 
    and the implementation could be the underlying operating system code (API) which the GUI layer calls in response to user interactions.

    Generally speaking, you can extend such an app in two independent directions:
    1. Have several different GUIs (for instance, tailored for regular customers or admins).
    2. Support several different APIs (for example, to be able to launch the app under Windows, Linux, and MacOS).
    */

    /// <summary>
    /// Bridge is a structural design pattern that lets you split a large class or a set of closely related classes into 
    /// two separate hierarchies—abstraction and implementation—which can be developed independently of each other.
    /// </summary>
    class BridgePattern : DesignPattern
    {
        public override string PatternName => "Bridge";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var tv = new TV();

            var remote = new AdvancedRemote(tv);
            remote.TooglePower();
            remote.ChannelUp();
            remote.VolumeDown();
            remote.Mute();


            var radio = new Radio();
            var radioRemote = new Remote(radio);
            radioRemote.TooglePower();
            radioRemote.ChannelDown();
        }
    }


    /// <summary>
    /// Implementation
    /// </summary>
    interface IDevice
    {
        bool IsEnabled();
        void Enable();
        void Disable();
        int GetVolume();
        void SetVolume(int volume);
        int GetChannel();
        void SetChanel(int chaneel);
    }

    class TV : IDevice
    {
        public void Disable()
        {
            Console.WriteLine("TV disabled");
        }

        public void Enable()
        {
            Console.WriteLine("TV enabled");
        }

        public int GetChannel()
        {
            Console.WriteLine("TV get chanel");
            return new Random().Next(10);
        }

        public int GetVolume()
        {
            Console.WriteLine("TV get volume");
            return new Random().Next(100);
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void SetChanel(int chaneel)
        {
            Console.WriteLine("TV set channel");
        }

        public void SetVolume(int volume)
        {
            Console.WriteLine("TV set volume");
        }
    }

    class Radio : IDevice
    {
        public void Disable()
        {
            Console.WriteLine("Radio disabled");
        }

        public void Enable()
        {
            Console.WriteLine("Radio enabled");
        }

        public int GetChannel()
        {
            Console.WriteLine("Radio get chanel");
            return new Random().Next(10);
        }

        public int GetVolume()
        {
            Console.WriteLine("Radio get volume");
            return new Random().Next(100);
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void SetChanel(int chaneel)
        {
            Console.WriteLine("Radio set channel");
        }

        public void SetVolume(int volume)
        {
            Console.WriteLine("Radio set volume");
        }
    }

    class Remote
    {
        protected IDevice _device;

        public Remote(IDevice device)
        {
            _device = device;
        }

        public void TooglePower()
        {
            if (_device.IsEnabled())
                _device.Disable();
            else
                _device.Enable();
        }

        public void VolumeDown()
        {
            var volume = _device.GetVolume();
            _device.SetVolume(volume - 1);
        }

        public void VolumeUp()
        {
            var volume = _device.GetVolume();
            _device.SetVolume(volume + 1);
        }

        public void ChannelDown()
        {
            var channel = _device.GetChannel();
            _device.SetChanel(channel - 1);
        }

        public void ChannelUp()
        {
            var channel = _device.GetChannel();
            _device.SetChanel(channel + 1);
        }
    }

    class AdvancedRemote : Remote
    {
        public AdvancedRemote(IDevice device) : base(device)
        {
        }

        public void Mute()
        {
            _device.SetVolume(0);
        }
    }
}
