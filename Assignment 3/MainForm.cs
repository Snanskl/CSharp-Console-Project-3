using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMICalculator
{
    public partial class MainForm : Form
    {
        #region fields for BMI
        private string name = string.Empty;
        private BMIClass bmiObjct = new BMIClass();
        private SavingPlanCalc savingObj = new SavingPlanCalc();
        private BMRCalc bmrObj = new BMRCalc();
        #endregion

        #region Program main function
        public MainForm()
        {
            InitializeComponent();

            InitializeGUI();
        }        
        private void InitializeGUI() 
        {
            //BMI functions
            bmiOoutputbox.Text = string.Empty;
            weightLbOutput.Text = "";
            imperialBtn.Checked = true; // radioBtn to be at least 1 checked
            generalBMI.Text = "Normal BMI is between  18.50 and 24.9";

            //Saving plan functions
            amountPaidOutput.Text = string.Empty;
            amountEarnTxtBoxOutput.Text = string.Empty;
            finalBalOutput.Text = string.Empty;
            totalFeesTxtBoxOutput.Text = string.Empty;

            //BMR function
            caloriesDayOutpuy.Text = string.Empty;
            maintainOutput.Text = string.Empty;
            lose500grOutput.Text = string.Empty;
            lose1kgOutput.Text = string.Empty;
            gain500grOutput.Text = string.Empty;
            gain1kgOutput.Text = string.Empty;
        }
        //Exit the app
        private void exitBtn_Click(object sender, EventArgs e)
        {Application.Exit(); }  
        private void MainForm_Load(object sender, EventArgs e)
        {CenterToScreen();}
        //Reset the app
        private void resetBtn_Click(object sender, EventArgs e)
        {
            //BMI function
            nameTextBox.Clear();
            heightTextboxCMOrFt.Clear();
            heightTxtBoxInch.Clear();
            WeightTextbox.Clear();
            bmiOoutputbox.Text = string.Empty;
            weightLbOutput.Text = string.Empty;
            resultGroupBox.Text = "Results : ";
            rangeWeightOutput.Text = string.Empty;

            //saving plan function
            monthlyDepTxtBox.Clear();
            periodTxtBox.Clear();
            amountPaidOutput.Text = string.Empty;
            finalBalOutput.Text = string.Empty;
            initialTxtBox.Clear();
            growthTxtBox.Clear();
            feesTxtBox.Clear();
            amountEarnTxtBoxOutput.Text = string.Empty;
            totalFeesTxtBoxOutput.Text = string.Empty;

            //BMR functions
            ageTxtBox.Clear();
            caloriesDayOutpuy.Text = string.Empty;
            maintainOutput.Text = string.Empty;
            lose500grOutput.Text = string.Empty;
            lose1kgOutput.Text = string.Empty;
            gain500grOutput.Text = string.Empty;
            gain1kgOutput.Text = string.Empty;
        }
        #endregion

        #region all the button functions for BMI
        private void metricBtn_CheckedChanged(object sender, EventArgs e)
        {UpdateHeight(); }//Update height unit when user change in radio button
        private void imperialBtn_CheckedChanged(object sender, EventArgs e)
        {UpdateHeight(); }//Update height unit when user change in radio button
        private void calculateBMIBtn_Click(object sender, EventArgs e)
        {
            // This function purpose is to first validate other function that have been called
            //if the validation is corrected, the output will be displayed by DisplayResultsBMI
            bool validBool = ReadInputBMI();

            if (validBool == true)
            { DisplayResultsBMI(); }
        }
        private void weightLbOutput_Click(object sender, EventArgs e)
        {/*weightLbOutput.Text = bmiObjct.BmiWeightCategory();*/}
        private void rangeWeightOutput_Click(object sender, EventArgs e)
        {/* rangeWeightOutput.Text = bmiObjct.NormalWeightOutput(); */}

        #endregion

        #region BMI methods // done
        public void DisplayResultsBMI()
        {
            //if ReadInput is validated to be true, the result from all these functions 
            //will be display
            bmiOoutputbox.Text = bmiObjct.Calculate().ToString("0.00");

            weightLbOutput.Text = bmiObjct.BmiWeightCategory(); //return string of which category user belongs to

            rangeWeightOutput.Text = bmiObjct.NormalWeightOutput(); //return string of a range recommendation of normal weight
        }
        private void UpdateHeight()
        {
            //changing label text based on user choice of unit measurement
            if (metricBtn.Checked) 
            {
                heightLebel.Text = "Height (cm)";
                weightLebel.Text = "Weight (kg)";
                heightTxtBoxInch.Visible = false;
            }
            else
            { 
                heightLebel.Text = "Height (ft, in)";
                weightLebel.Text = "Weight (lbs)";
                heightTxtBoxInch.Visible = true;
            }
        }
        private bool ReadHeight()
        {
            double height = 0.0;
            double inch = 0.0;
            bool validHeight = double.TryParse(heightTextboxCMOrFt.Text, out height);//Will determine input true or false

            if (metricBtn.Checked)
            { 
                if (validHeight == false)
                { MessageBox.Show("The height value you've given are not valid", "Oops!"); }

                else if (validHeight == true)
                {
                    if (height > 0)
                    {
                        if (bmiObjct.GetUnit() == UnityTypes.Metric) // set unit in metric
                        { 
                            bmiObjct.SetHeight(height / 100.0); 
                            bmrObj.SetHeight(height / 100.0);
                        }
                    }
                }
            }

            if (imperialBtn.Checked)
            {
                validHeight = validHeight && double.TryParse(heightTxtBoxInch.Text, out inch);

                if (validHeight == false)
                { MessageBox.Show("The inch value you've given are not valid", "Oops!"); }

                else if (validHeight == true)
                {
                    if (height > 0 && inch >= 0) // validate height and inch input
                    {
                        if (bmiObjct.GetUnit() == UnityTypes.Imperial)
                        {
                            bmiObjct.SetHeight(height * 12.0 + inch); 
                            bmrObj.SetHeight(height * 12.0 + inch);
                        }
                    }
                }
            }
            return validHeight;
        }
        private bool ReadWeight()
        {
            //read user weight input, validate it and set it toBMR and BMI class
            double weight = 0.0;
            bool validWeight = double.TryParse(WeightTextbox.Text, out weight);

            if (validWeight == false)
            {MessageBox.Show("The weight value you've given is not valid", "Opps!");}

            else
            {
                bmiObjct.SetWeight(weight);
                bmrObj.SetWeight(weight);
            }
            
            return validWeight;
        }
        private void ReadUnit()
        {
            if (metricBtn.Checked)
            { bmiObjct.SetUnit(UnityTypes.Metric); }
            else
            { bmiObjct.SetUnit(UnityTypes.Imperial); }
        }
        private bool ReadInputBMI()
        {
            ReadName();    
            ReadUnit();
            bool validHeight = ReadHeight();
            bool validWeight = ReadWeight();
        
            return validHeight && validWeight;
        }
        private void ReadName()
        {
            name = nameTextBox.Text.Trim(); // trim name input for reducing white space
            resultGroupBox.Text = $"Results for {name}"; // display user name input

            if (nameTextBox.Text == "") //if name textbox is empty and using string as a default
            { 
                bmiObjct.SetName("No name");
                resultGroupBox.Text = "Results : No name";
            }
            else
            {
                bmiObjct.SetName(nameTextBox.Text); // otherwise take user name input and set it
            }
        }
        #endregion



        #region all the button functions for Saving plan
        private void periodTextbox_Click(object sender, EventArgs e)
        {}
        private void calculateBtn_Click(object sender, EventArgs e)
        {
            //this function is for validating ReadInput function and if success, display DisplayResult function
            bool validBoolSaving = ReadInputSaving();

            if (validBoolSaving == true)
            { DisplayResultSaving(); }
        }
        #endregion

        #region Saving plan methods 
        public void DisplayResultSaving()
        {
            //call on functions if ReadInput function successfully validated
            amountPaidOutput.Text = savingObj.CalculateAmountPaid().ToString("F");

            amountEarnTxtBoxOutput.Text = savingObj.CalculateAmountEarned().ToString("F");

            finalBalOutput.Text = savingObj.CalculateFinalBalance().ToString("F");

            totalFeesTxtBoxOutput.Text = savingObj.CalculateTotalFees().ToString("F");
        }
        private bool ReadInputSaving()
        {
            //validating each function and return the bool value
            bool validDepo = ReadInitialDepo();
            bool validMonthlyDepo = ReadMonthlyDep();
            bool validPeriod = ReadPeriod();
            bool validGrowthIntrs = ReadGrowthInterest();
            bool validFees = ReadFees();

            return validDepo && validMonthlyDepo && validPeriod && validGrowthIntrs && validFees;
        }
        private bool ReadInitialDepo()
        {
            double initialDepo = 0.0;
            bool validInitialDepo = double.TryParse(initialTxtBox.Text, out initialDepo); //will turn true if value is numerical

            if (validInitialDepo == false)
            { MessageBox.Show("The initial deposit value you've given is invalid", "Oops!"); }
            else
            { savingObj.SetInitialDepo(initialDepo); }

            return validInitialDepo;
        }
        private bool ReadMonthlyDep()
        { 
            double monthlyDep = 0.0;
            bool validMonthlyDepo = double.TryParse(monthlyDepTxtBox.Text, out monthlyDep); //convert string to double and validate with bool

            if (validMonthlyDepo == false)
            { MessageBox.Show("The monthly deposit value you've given is invalid", "Oops!"); }
            else
            { savingObj.SetMonthlyDepo(monthlyDep); }

            return validMonthlyDepo;
        }
        private bool ReadPeriod()
        {
            int period = 0;
            bool validPeriod = int.TryParse(periodTxtBox.Text, out period);

            if (validPeriod == false)
            { MessageBox.Show("The period (year) you've given is invalid", "Oops!"); }
            else
            { savingObj.SetPeriod(period); }

            return validPeriod;
        }
        private bool ReadGrowthInterest()
        {
            double growthInterst = 0.0;
            bool validGrowthIntrs = double.TryParse(growthTxtBox.Text, out growthInterst);

            if (validGrowthIntrs == false)
            { MessageBox.Show("The growth interest value you've given is invalid", "Oops!"); }
            else
            { savingObj.SetGrowthRate(growthInterst); }

            return validGrowthIntrs;
        }
        private bool ReadFees()
        {
            double fees = 0.0;
            bool validFees = double.TryParse(feesTxtBox.Text, out fees); //convert string to double and validate with bool 

            if (validFees == false)
            { MessageBox.Show("The fees value you've given is invalid", "Oops!"); }
            else
            { savingObj.SetFeesRate(fees); }//if true, sent double to set function
        
            return validFees;
        }

        #endregion



        #region all the button functions for BMR 
        private void calculateBMRBtn_Click(object sender, EventArgs e) //done
        {
            //Validate ReadInput function and if success, display DisplayResult
            bool validBoolBMR = ReadInputBMR();

            if (validBoolBMR == true)
            { DisplayResultBMR(); }
        }
        #endregion

        #region BMR methods
        private bool ReadInputBMR() 
        {   
            //validate the textbox function to prevent typo or error, the other two are radio button
            //return bool value of each function if success
            ReadActivity();
            ReadGender();
            bool validWeight = ReadWeight();
            bool validHeight = ReadHeight();
            bool validAge = ReadAge();

            return validAge && validHeight && validWeight;
        }
        public void DisplayResultBMR()
        {
            //this function will be called if all the bool value are validated to be true
            caloriesDayOutpuy.Text = bmrObj.BMRCalculation().ToString("F2");
            maintainOutput.Text = bmrObj.CaloriesPerday().ToString("F2");
            lose500grOutput.Text = bmrObj.LoseWeight500gr().ToString("F2");
            lose1kgOutput.Text = bmrObj.LoseWeight1000gr().ToString("F2");
            gain500grOutput.Text = bmrObj.GainWeight500gr().ToString("F2");
            gain1kgOutput.Text = bmrObj.GainWeight1000gr().ToString("F2");
        }
        #endregion

        #region Read all the data of BMR
        private void ReadGender() //done
        {
            if (femalRadBtn.Checked)
            { bmrObj.SetGender(GenDerenumClass.Female); }

            else if (maleRadBtn.Checked)
            { bmrObj.SetGender(GenDerenumClass.Male); }
        }
        private void ReadActivity() //done
        {
            //set activity level and send to set function in BMR class
            double activityLevel = 0.0;

            if (sedentaryLevelRadBtn.Checked)
            {
                activityLevel = 1.2;
                bmrObj.SetActivity(activityLevel);
            }

            else if (lightlyLevelRadBtn.Checked)
            {
                activityLevel = 1.375;
                bmrObj.SetActivity(activityLevel);
            }

            else if (moderateLevelRadBtn.Checked)
            {
                activityLevel = 1.55;
                bmrObj.SetActivity(activityLevel);
            }

            else if (veryActiveRadBtn.Checked)
            {
                activityLevel = 1.725;
                bmrObj.SetActivity(activityLevel);
            }

            else if (extraActiveRadBtn.Checked)
            {
                activityLevel = 1.9;
                bmrObj.SetActivity(activityLevel);
            }
        }
        private bool ReadAge() //done
        {
            int age = 0;
            bool validAge = int.TryParse(ageTxtBox.Text, out age); // turn string to int and validate with bool

            if ((validAge = false) || (age <= 0)) //if input is not int or input is equal to or less then 0, error
            { MessageBox.Show("The age value you've given is not valid", "Opps!"); }

            else if ((validAge = true) || (age > 0)) // otherwise, send to set function
            { bmrObj.SetAge(age); }
            
            return validAge;
        }
        #endregion
    }

}