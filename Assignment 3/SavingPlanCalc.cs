using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    internal class SavingPlanCalc
    {
        #region fields area
        private double p_initialDeposit = 0.0;
        private double monthlyDeposit  = 0.0;
        private int t_period = 0;
        private double r_interestGrowthRate = 0.0;
        private double feesRate = 0.0;
        private int compoundingFreq = 12;
        private double balance = 0.0;

        #endregion

        #region Getter and Setter area // done
        //Getter for read only
        //Setter for modify value only
        public double GetInitialDepo()
        { return p_initialDeposit; }
        public void SetInitialDepo(double initialDeposit)
        {
            if (initialDeposit > 0)
            { p_initialDeposit = initialDeposit; }
        }
        public double GetMonthlyDepo()
        { return monthlyDeposit; }
        public void SetMonthlyDepo(double monthlyDepo)
        {
            if (monthlyDepo > 0) // may have to fix this to >=
            {this.monthlyDeposit = monthlyDepo;}
        }
        public int GetPeriod()
        { return t_period; }
        public void SetPeriod(int period)
        {
            if (period > 0)
            { t_period = period; }
        }
        public double GetGrowthRate()
        { return r_interestGrowthRate; }
        public void SetGrowthRate(double growthRate)
        {
            if (growthRate > 0)
            { r_interestGrowthRate = growthRate; }
        }
        public double GetFeesRate()
        { return feesRate; }
        public void SetFeesRate(double feesRate)
        {
            if (feesRate > 0)
            { this.feesRate = feesRate; }
        }

        #endregion 

        #region Calculation area
        private void CompoundCalculation()
        {
            balance = p_initialDeposit;
            double interestRatePerCompound = (r_interestGrowthRate/100) / compoundingFreq;
            double feeRatePerCompunding = (feesRate / 100) / compoundingFreq;

            balance = p_initialDeposit;
            for (int i = 0; i < t_period * compoundingFreq; i++)
            {
                balance = (balance + monthlyDeposit) 
                        * (1 + interestRatePerCompound)
                        - (balance + monthlyDeposit) 
                        * feeRatePerCompunding;
            }
        }
        public double CalculateAmountPaid()
        {
            CompoundCalculation();
            double outputAmountPaid = 0.0;
            outputAmountPaid = (p_initialDeposit + monthlyDeposit * t_period * compoundingFreq);

            return outputAmountPaid;
        }
        public double CalculateAmountEarned()
        {
            balance = p_initialDeposit;
            CompoundCalculation();
            double outputAmountEarned = 0.00;
            outputAmountEarned = (balance - p_initialDeposit - monthlyDeposit * t_period * compoundingFreq);

            return outputAmountEarned;
        }
        public double CalculateFinalBalance()
        {
            balance = p_initialDeposit;
            CompoundCalculation();
            double outputFinalBal = balance;
            return outputFinalBal;
        }
        public double CalculateTotalFees()
        {
            CompoundCalculation();
            double outputTotalFees = 0.00;
            outputTotalFees = (p_initialDeposit + monthlyDeposit * t_period * compoundingFreq) * feesRate;

            return outputTotalFees;
        }
        #endregion
    }
}
