using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    internal class BMRCalc
    {
        #region fields area
        private int age = 0;
        private double height = 0;
        private double weight = 0;
        private double activity = 0.0;
        private GenDerenumClass genderenum = new GenDerenumClass();
        private UnityTypes unit = new UnityTypes();
        #endregion

        #region Getter and setter
        public double GetHeight() //done
        {return height;}
        public void SetHeight(double height)//done
        {
            if (height > 0)
            { this.height = height; } 
        }
        public double GetWeight() //done
        { return weight; }
        public void SetWeight(double weight) //done
        {
            if (weight > 0)
            {this.weight = weight;}
        }
        public double GetAge() //done
        { return age; }
        public void SetAge(int age) //done
        {
            if (age > 0)
            { this.age = age; }
        }
        public double GetActivity()//done
        { return activity; }
        public void SetActivity(double activityLevel) //done
        { activity = activityLevel;}
        public GenDerenumClass GetGender() //done
        { return genderenum; }
        public void SetGender(GenDerenumClass gender) //done
        {genderenum = gender; }

        public UnityTypes GetUnit()
        { return unit; }
        public void SetUnit(UnityTypes unitOpt)
        { unit = unitOpt; }

        #endregion

        #region Calculate on BMR value
        public double BMRCalculation() //done
        {
            double bmr = 0;

            //Metric
            if (genderenum == GenDerenumClass.Female)
            {
                if (unit == UnityTypes.Metric)
                { bmr = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age); }
                else
                {bmr = 655.1 + (4.35 * weight) + (4.7 * height) - (4.7 * age);  }
            } //done

            else if (genderenum == GenDerenumClass.Male)
            {
                if (unit == UnityTypes.Metric)
                { bmr = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age); }
                else
                { bmr = 66 + (6.2 * weight) + (12.7 * height) - (6.76 * age); }
            } //done

            return bmr;
        }
        public double CaloriesPerday() //done
        {
            //Calculate BMR with activity level
            double caloriesPerDay = BMRCalculation() * activity;

            return caloriesPerDay;
        }
        public double LoseWeight500gr()
        {
            double loseWeight500gr = CaloriesPerday() - 500;

            return loseWeight500gr;
        }
        public double LoseWeight1000gr()
        {
            double loseWeight1000gr = CaloriesPerday() - 1000;

            return loseWeight1000gr;
        }
        public double GainWeight500gr()
        {
            double gainWeight500gr = CaloriesPerday() + 500;

            return gainWeight500gr;
        }
        public double GainWeight1000gr()
        {
            double gainWeight1000gr = CaloriesPerday() + 1000;

            return gainWeight1000gr;
        }

        #endregion
    }
}
