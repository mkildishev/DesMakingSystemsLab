﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DesMakingSystemsLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        void DrawFunction(Function func)
        {
            chart1.Series.Clear();
            double a_border = Convert.ToDouble(textBox8.Text);
            double b_border = Convert.ToDouble(textBox9.Text);
            chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(textBox8.Text);
            chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(textBox9.Text);
            Series series = new Series("Функция");
            series.ChartType = SeriesChartType.Spline;
            series.Color = Color.Black;
            for (double i = a_border; i <= b_border; i+=0.01)
            {
                series.Points.AddXY(i, func.GetFunctionValue(i));
            }
            chart1.Series.Add(series);
        }

        void DrawPoints(List<double> points_of_trial, int max_R)
        {
            chart2.Series.Clear();
            chart2.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(textBox8.Text);
            chart2.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(textBox9.Text);
            Series series_points = new Series("Точки испытаний");
            series_points.ChartType = SeriesChartType.Point;
            series_points.Color = Color.Red;
            for (int i = 0; i < points_of_trial.Count(); i++)
                series_points.Points.AddXY(points_of_trial[i], 0);
            chart2.Series.Add(series_points);
            Series last_point = new Series("Последняя точка");
            last_point.ChartType = SeriesChartType.Point;
            last_point.Color = Color.Green;
            last_point.Points.AddXY(points_of_trial[max_R], 0);
            chart2.Series.Add(last_point);
        }

        bool IsCorrect()
        {
            double a_border = Convert.ToDouble(textBox8.Text);
            double b_border = Convert.ToDouble(textBox9.Text);
            if (a_border > b_border)
                return false;
            else return true;
        }

        void InitFunction(Function func)
        {
            double a = Convert.ToDouble(textBox1.Text);
            double b = Convert.ToDouble(textBox3.Text);
            double c = Convert.ToDouble(textBox2.Text);
            double d = Convert.ToDouble(textBox4.Text);
            func.SetFunctionParams(a, c, b, d);
        }

        void PrintResult(ComputationalMethod meth, Function func)
        {
            richTextBox1.Text = "Количество испытаний: ";
            richTextBox1.Text += Convert.ToString(meth.count_of_trials);
            richTextBox1.Text += "\n";
            richTextBox1.Text += "Количество измерений функции: ";
            richTextBox1.Text += Convert.ToString(meth.count_of_measurement_function);
            richTextBox1.Text += "\n";
            richTextBox1.Text += "Координата минимального значения функции: ";
            richTextBox1.Text += Convert.ToString(meth.serial_of_trials[meth.num_of_max_interval_characteristics]);
            richTextBox1.Text += "\n";
            richTextBox1.Text += "Минимальное значение функции: ";
            richTextBox1.Text += Convert.ToString(func.GetFunctionValue(meth.serial_of_trials[meth.num_of_max_interval_characteristics]));
            richTextBox1.Text += "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Function func = new Function();
            ComputationalMethod meth;
            InitFunction(func);
            if (IsCorrect())
                DrawFunction(func);
            double a_border = Convert.ToDouble(textBox8.Text);
            double b_border = Convert.ToDouble(textBox9.Text);
            double stop;
            if (radioButton4.Checked)
                stop = Convert.ToDouble(textBox10.Text);
            else
                stop = Convert.ToDouble(textBox11.Text);
            double r = Convert.ToDouble(textBox5.Text);
            if (radioButton1.Checked)
            {
                meth = new ComputationalMethod();
                r = Convert.ToDouble(textBox7.Text);
                meth.Init(func, a_border, b_border, radioButton4, stop, r);
                meth.Start();
                DrawPoints(meth.serial_of_trials, meth.num_of_max_interval_characteristics);
                PrintResult(meth,func);
            }
            else if (radioButton2.Checked)
            {
                meth = new PiyavskyMethod();
                meth.Init(func, a_border, b_border, radioButton4, stop, r);
                meth.Start();
                DrawPoints(meth.serial_of_trials, meth.num_of_max_interval_characteristics);
                PrintResult(meth,func);
            }
            else if(radioButton3.Checked)
            {
                meth = new StronginMethod();
                meth.Init(func, a_border, b_border, radioButton4, stop, r);
                meth.Start();
                DrawPoints(meth.serial_of_trials, meth.num_of_max_interval_characteristics);
                PrintResult(meth,func);
            }
            
        }

     
    }
    
}
