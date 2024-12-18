using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MoneyWF
{
    public partial class Form1 : Form
    {
        private MoneyContainer container = new MoneyContainer();

        private void UpdateTextBoxOutPut()
        {
            textBoxOutput.Clear();

            foreach (var money in container.GetAllMoney())
            {
                textBoxOutput.AppendText(money.ToString() + Environment.NewLine + Environment.NewLine);
            }
        }

        private void ClearInputFields()
        {
            textBoxName.Clear();
            textBoxAmount.Clear();
            textBoxRate.Clear();
            textBoxTime.Clear();
            textBoxBonusRate.Clear();
            textBoxSelectedName.Clear();
            textBoxAmountChange.Clear();
        }

        public Form1()
        {
            InitializeComponent();

        }

        private void buttonAddDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text;
                double amount = double.Parse(textBoxAmount.Text);
                double rate = double.Parse(textBoxRate.Text);
                int time = int.Parse(textBoxTime.Text);

                container.AddMoney(new Deposit(name, amount, rate, time));

                UpdateTextBoxOutPut();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error");
            }
        }

        private void buttonAddSpecialDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text;
                double amount = double.Parse(textBoxAmount.Text);
                double rate = double.Parse(textBoxRate.Text);
                int time = int.Parse(textBoxTime.Text);
                double bonusRate = double.Parse(textBoxBonusRate.Text);

                container.AddMoney(new SpecialDeposit(name, amount, rate, time, bonusRate));

                UpdateTextBoxOutPut();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error");
            }
        }

        private void buttonAddAmount_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxSelectedName.Text;
                double amountToAdd = double.Parse(textBoxAmountChange.Text);

                foreach (var money in container.GetAllMoney())
                {
                    if (money is MoneyManagement mm && mm.Name == name)
                    {
                        mm.AddAmount(amountToAdd);
                        UpdateTextBoxOutPut();
                        ClearInputFields();
                        return;
                    }

                }

                MessageBox.Show("No matching MoneyManagement object found", "Selection Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error{ex.Message}", "Input Error");
            }
        }

        private void buttonWithdrawAmount_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxSelectedName.Text;
                double amountToWithdraw = double.Parse(textBoxAmountChange.Text);

                foreach (var money in container.GetAllMoney())
                {
                    if (money is MoneyManagement mm && mm.Name == name)
                    {
                        mm.WithdrawAmount(amountToWithdraw);
                        UpdateTextBoxOutPut();
                        ClearInputFields();
                        return;
                    }
                }

                MessageBox.Show("No matching MoneyManagement object found", "Selection Error");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Not enough money");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error");
            }
        }

        private void buttonMoneyManagment_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxName.Text;
                double amount = double.Parse(textBoxAmount.Text);
                double rate = double.Parse(textBoxRate.Text);
                int time = int.Parse(textBoxTime.Text);

                container.AddMoney(new MoneyManagement(name, amount, rate, time));

                UpdateTextBoxOutPut();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error");
            }
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            textBoxOutput.Clear();

            foreach (var money in container.GetAllMoney())
            {
                textBoxOutput.AppendText(money.ToString() + Environment.NewLine + Environment.NewLine);
            }

            if (container.GetAllMoney().Count == 0)
            {
                MessageBox.Show("No deposits to show", "Information");
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            container.ClearAll();
            textBoxOutput.Clear();
        }

        private void buttonTotalAmount_Click(object sender, EventArgs e)
        {
            double total = container.CalculateTotal();
            labelTotalAmount.Text = $"Total amount: {total:C}";
        }

    }
}
