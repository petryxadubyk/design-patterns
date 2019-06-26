using System;

namespace design_patterns
{
    /// <summary>
    /// Mediator is a behavioral design pattern that lets you reduce chaotic dependencies between objects. 
    /// The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object.
    /// </summary>
    class MediatorPattern : DesignPattern
    {
        public override string PatternName => "Mediator";
        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var authenticationDialog = new AuthenticationDialog();
            authenticationDialog.Login();
        }
    }

    interface IMediator
    {
        void Notify(Component sender, string eventData);
    }

    class AuthenticationDialog : IMediator
    {
        private Button _button;
        private TextBox _usernameTextBox;
        private TextBox _passwordTextBox;
        private CheckBox _checkbox;

        public AuthenticationDialog()
        {
            _button = new Button(this);
            _usernameTextBox = new TextBox(this);
            _passwordTextBox = new TextBox(this);
            _checkbox = new CheckBox(this);
        }

        public void Notify(Component sender, string eventData)
        {
            if(sender == _button && eventData == "Clicked")
            {
                ReactOnLogin();
            }
            if(sender == _checkbox && eventData == "Checked")
            {
                ReactOnCheckBox();
            }
        }

        void ReactOnCheckBox()
        {
            _button.Disable();
        }

        void ReactOnLogin()
        {
            _usernameTextBox.Clear();
            _passwordTextBox.Clear();
            _button.Disable();
        }

        public void Login()
        {
            _button.Click();
            _checkbox.Check();
        }
    }

    abstract class Component
    {
        protected IMediator _mediator;
        public Component(IMediator mediator)
        {
            _mediator = mediator;
        }
        abstract public void Click();
        abstract public void KeyPress();
    }

    class Button : Component
    {
        public Button(IMediator mediator) : base(mediator)
        {
        }

        public override void Click()
        {
            _mediator.Notify(this, "Clicked");
        }

        public override void KeyPress()
        {
            _mediator.Notify(this, "Key pressed");
        }

        public void Disable()
        {
            Console.WriteLine("Disabled");
        }
    }

    class TextBox : Component
    {
        public TextBox(IMediator mediator) : base(mediator)
        {
        }

        public override void Click()
        {
            _mediator.Notify(this, "Clicked");
        }

        public override void KeyPress()
        {
            _mediator.Notify(this, "Key pressed");
        }

        public void Clear()
        {
            Console.WriteLine("Cleared");
        }
    }

    class CheckBox : Component
    {
        public CheckBox(IMediator mediator) : base(mediator)
        {
        }

        public override void Click()
        {
            _mediator.Notify(this, "Clicked");
        }

        public override void KeyPress()
        {
            _mediator.Notify(this, "Key pressed");
        }

        public void Check()
        {
            _mediator.Notify(this, "Checked");
        }
    }
}
