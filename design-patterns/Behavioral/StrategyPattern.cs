using System;

namespace design_patterns
{
    /*
    The Strategy pattern suggests that you take a class that does something specific in a lot of different ways 
    and extract all of these algorithms into separate classes called strategies.
    
    The original class, called context, must have a field for storing a reference to one of the strategies. 
    The context delegates the work to a linked strategy object instead of executing it on its own.

    The context isn’t responsible for selecting an appropriate algorithm for the job. 
    Instead, the client passes the desired strategy to the context. 
    */
    /// <summary>
    /// Strategy is a behavioral design pattern that lets you define a family of algorithms, 
    /// put each of them into a separate class, and make their objects interchangeable.
    /// </summary>
    class StrategyPattern : DesignPattern
    {
        public override string PatternName => "Strategy";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var swiftPayment = new SwiftPayment();
            var mastercardPayment = new MaterCardPayment();

            var transaction = new BankTransaction("me", "you", 100000);
            transaction.SetPaymentMethod(swiftPayment);
            transaction.CreateTreansaction();
            transaction.GetTransactionStatus();

            transaction.SetPaymentMethod(mastercardPayment);
            transaction.CreateTreansaction();
            transaction.GetTransactionStatus();
        }
    }

    class Payment
    {
        public int Amount { get; set; }
        public string Recipient { get; set; }
        public string Payer { get; set; }
    }

    interface IPaymentStrategy
    {
        bool VerifyPayment(Payment payment);
        void MakePayment(Payment payment);
        int CheckPaymentStatus(Payment payment);
    }

    class SwiftPayment : IPaymentStrategy
    {
        public int CheckPaymentStatus(Payment payment)
        {
            Console.WriteLine("Checked swift payment status");
            return 1;
        }

        public void MakePayment(Payment payment)
        {
            Console.WriteLine($"Swift payment: From {payment.Payer} To {payment.Recipient}. Amount: {payment.Amount}");
        }

        public bool VerifyPayment(Payment payment)
        {
            Console.WriteLine("Verified swift payment data is valid");
            return true;
        }
    }

    class MaterCardPayment : IPaymentStrategy
    {
        public int CheckPaymentStatus(Payment payment)
        {
            Console.WriteLine("Checked mastercard payment status");
            return 1;
        }

        public void MakePayment(Payment payment)
        {
            Console.WriteLine($"Mastercard payment: From {payment.Payer} To {payment.Recipient}. Amount: {payment.Amount}");
        }

        public bool VerifyPayment(Payment payment)
        {
            Console.WriteLine("Verified mastercard payment data is valid");
            return true;
        }
    }

    class BankTransaction
    {
        private IPaymentStrategy _paymentStrategy;
        private string _from;
        private string _to;
        private int _amount;
        private Payment _payment;

        public BankTransaction(string from, string to, int amount)
        {
            _from = from;
            _to = to;
            _amount = amount;
        }

        public void SetPaymentMethod(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void CreateTreansaction()
        {
            _payment = new Payment()
            {
                Amount = _amount,
                Payer = _from,
                Recipient = _to
            };
            _paymentStrategy.VerifyPayment(_payment);
            _paymentStrategy.MakePayment(_payment);
        }

        public int GetTransactionStatus()
        {
            return _paymentStrategy.CheckPaymentStatus(_payment);
        }
    }
}
