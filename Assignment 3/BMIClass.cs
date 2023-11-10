using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    internal class BMIClass
    {
        #region fields area 
        private double height = 0.0;
        private double weight = 0.0;
        private string name = "No name";
        UnityTypes unit = new UnityTypes();
        #endregion
         
        #region get and set //done
        public double GetHeight()
        {return height;}
        public void SetHeight(double height)
        {
            //using ternary operator for shorter syntax
            this.height = height >= 0.0 ? height : this.height; 
            //height param set to height filed if argument is > or = to 0
            //otherwise will get users height
        }

        public double GetWeight()
        {return weight;}
        public void SetWeight(double weight)
        {
            this.weight = weight >= 0.0 ? weight : this.weight;
        }

        public string GetName()
        {return name;}
        public void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name)) 
            { this.name = name;}
        }

        public UnityTypes GetUnit()
        {return unit;}
        public void SetUnit(UnityTypes unitOpt)
        {unit = unitOpt;}

        #endregion 

        #region weight category // done
        public string BmiWeightCategory()
        {
            string weightString = string.Empty;
            double compareBMI = Calculate();

            if (compareBMI < 18.9)
            { weightString = "Underweight"; }

            else if (compareBMI <= 24.9)
            { weightString = "Normal weight"; }

            else if (compareBMI <= 29.9)
            {weightString = "Overweight(Pre-obesity)";}

            else if (compareBMI <= 34.9)
            {weightString = "Overweight(Obesity class 1)";}

            else if (compareBMI <= 39.9)
            { weightString = "Overweight(Obesity class 2)";}

            else 
            { weightString = "Overweight(Obesity class 3)";}

            return weightString;
        }
        public string NormalWeightOutput()
        { 
            string normalWeightReturn = string.Empty;
            double lowBmi= 0.0; //for calculate reccommended the lowest rate calories to user
            double highBmi = 0.0;//for calculate reccommended the highest rate calories to user

            if (unit == UnityTypes.Metric)
            {
                lowBmi = 18.5 * Math.Pow(height / 100, 2);
                highBmi = 24.9 * Math.Pow(height / 100, 2);
                normalWeightReturn = $"Normal weight should be between {lowBmi.ToString("0.00")}kg and {highBmi.ToString("0.00")}kg";
            }
            else if (unit == UnityTypes.Imperial)
            {
                lowBmi = 18.5 * 703 / (height * height);
                highBmi = 25 * 703 / (height * height);
                normalWeightReturn = $"Normal weight should be between {lowBmi.ToString("0.00")}lbs and {highBmi.ToString("0.00")}lbs";
            }
            return normalWeightReturn;
        }
         
        #endregion

        #region calculate section // done
        public double Calculate()
        {
            double outPutVal = 0.0;

            if (unit == UnityTypes.Metric)
            { outPutVal = weight / (height * height); }

            else if (unit == UnityTypes.Imperial)
            { outPutVal = 703.0 * weight / (height * height); }

            return outPutVal;
        }
        #endregion
    }
}
