using System;

namespace design_patterns
{
    /*
     This structure may look similar to the Strategy pattern, but there’s one key difference. 
     In the State pattern, the particular states may be aware of each other and initiate transitions from one state to another, 
     whereas strategies almost never know about each other.
     */

    /// <summary>
    /// State is a behavioral design pattern that lets an object alter its behavior when its internal state changes. 
    /// It appears as if the object changed its class.
    /// </summary>
    class StatePattern : DesignPattern
    {
        public override string PatternName => "State";
        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var smartphone = new Smartphone();
            smartphone.ChangeState(new LockedState(smartphone));

            smartphone.PowerButtonClick();
            smartphone.PowerButtonClick();
            smartphone.PressButton();
            smartphone.PressButton();
            smartphone.PowerButtonClick();
            smartphone.VolumeButtonClick(30);
            smartphone.PressButton();
            smartphone.VolumeButtonClick(10);
        }
    }

    interface IState
    {
        void PowerButtonClick();
        void PressButton();
        void VolumeButtonClick();
    }

    abstract class State : IState
    {
        protected Smartphone _smartphone;
        public State(Smartphone smartphone)
        {
            _smartphone = smartphone;
        }

        abstract public void PowerButtonClick();
        abstract public void PressButton();
        abstract public void VolumeButtonClick();
    }

    class LockedState : State
    {
        public LockedState(Smartphone smartphone) : base(smartphone)
        {
        }

        public override void PowerButtonClick()
        {
            Console.WriteLine("Changed state to unlocked");
            _smartphone.ChangeState(new UnlockedState(_smartphone));
            return;
        }

        public override void PressButton()
        {
            Console.WriteLine("Press button: show locked screen");
            return;
        }

        public override void VolumeButtonClick()
        {
            Console.WriteLine("Press volume button: show locked screen");
            return;
        }
    }

    class UnlockedState : State
    {
        public UnlockedState(Smartphone smartphone) : base(smartphone)
        {
        }

        public override void PowerButtonClick()
        {
            Console.WriteLine("Changed state to locked");
            _smartphone.ChangeState(new LockedState(_smartphone));
        }

        public override void PressButton()
        {
            if (_smartphone.CheckBatteryCharge() < 10)
            {
                Console.WriteLine("Changed state to low battery");
                _smartphone.ChangeState(new LowBatteryState(_smartphone));
            }
            else
                Console.WriteLine("Proceed button click");
        }

        public override void VolumeButtonClick()
        {
            Console.WriteLine("Proceed volume button click");
        }
    }

    class LowBatteryState : State
    {
        public LowBatteryState(Smartphone smartphone) : base(smartphone)
        {
        }

        public override void PowerButtonClick()
        {
            Console.WriteLine("Show low battery screen");
        }

        public override void PressButton()
        {
            if (_smartphone.CheckBatteryCharge() >= 10)
            {
                Console.WriteLine("Changed state to unlocked");
                _smartphone.ChangeState(new UnlockedState(_smartphone));
            }
            else
                Console.WriteLine("Show low battery screen");
        }

        public override void VolumeButtonClick()
        {
            Console.WriteLine("Show low battery screen");
        }
    }

    class Smartphone
    {
        private IState _state;
        private int _volume = 100;

        public int CheckBatteryCharge()
        {
            return new Random().Next(100);
        }

        public void ChangeState(IState state)
        {
            _state = state;
        }

        public void PowerButtonClick()
        {
            _state.PowerButtonClick();
        }

        public void PressButton()
        {
            _state.PressButton();
        }

        public void VolumeButtonClick(int volume)
        {
            if (_volume < 100)
                _volume += volume;
            _state.VolumeButtonClick();
        }
    }
}
