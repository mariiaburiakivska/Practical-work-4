using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWF
{
    public abstract class Money
    {
        public string Name { get; set; }
        public double InitialAmount { get; set; }
        public double Rate { get; set; }
        public int Time { get; set; }

        protected Money(string name, double initialAmount, double rate, int time)
        {
            Name = name;
            InitialAmount = initialAmount;
            Rate = rate;
            Time = time;
        }

        public abstract double CalculateTotalAmount();

        public override string ToString()
        {
            return $"Name: {Name}{Environment.NewLine}" + 
                $"Amount: {InitialAmount:C}{Environment.NewLine}" + 
                $"Rate: {Rate}%{Environment.NewLine}" + 
                $"Time: {Time} years{Environment.NewLine}" + 
                $"Total: {CalculateTotalAmount():C}";
        }
    }


    public class Deposit : Money
    {
        public Deposit(string name, double initialAmount, double rate, int time)
            : base(name, initialAmount, rate, time) { }

        public override double CalculateTotalAmount()
        {
            return InitialAmount * Math.Pow(1 + Rate / 100, Time);
        }
    }



    public class SpecialDeposit : Money
    {
        public double BonusRate { get; set; }

        public SpecialDeposit(string name, double initialAmount, double rate, int time, double bonusRate)
            : base(name, initialAmount, rate, time)
        {
            BonusRate = bonusRate;
        }

        public override double CalculateTotalAmount()
        {
            double baseAmount = InitialAmount * Math.Pow(1 + Rate / 100, Time);

            if (Time > 5)
            {
                baseAmount += InitialAmount * (BonusRate / 100);
            }

            return baseAmount;
        }

        public override string ToString()
        {
            return base.ToString() + $"Bonus Rate: {BonusRate}%{Environment.NewLine}";
        }
    }


    public class MoneyManagement : Money
    {
        public MoneyManagement(string name, double initialAmount, double rate, int time)
            : base(name, initialAmount, rate, time) { }

        public void AddAmount(double amount)
        {
            InitialAmount += amount;
        }

        public void WithdrawAmount(double amount)
        {
            if (amount > InitialAmount)
                throw new InvalidOperationException("Not enough money");
            InitialAmount -= amount;
        }

        public override double CalculateTotalAmount()
        {
            return InitialAmount * Math.Pow(1 + Rate / 100, Time);
        }
    }


    public class MoneyContainer
    {
        private List<Money> moneyList = new List<Money>();

        public void AddMoney(Money money)
        {
            moneyList.Add(money);
        }

        public void ClearAll()
        {
            moneyList.Clear();
        }

        public List<Money> GetAllMoney()
        {
            return moneyList;
        }

        public double CalculateTotal()
        {
            return moneyList.Sum(m => m.CalculateTotalAmount());
        }

    }
}
