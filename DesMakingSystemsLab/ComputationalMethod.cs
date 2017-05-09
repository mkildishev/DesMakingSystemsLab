using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DesMakingSystemsLab
{
    class ComputationalMethod
    {
        public int count_of_trials;
        public int count_of_measurement_function;
        public int num_of_max_interval_characteristics;
        public double a_border;
        public double b_border;
        public double coord_of_min;
        public double min_value;
        public double stop_param;
        public List<double> serial_of_trials;
        public List<double> list_of_interval_characteristics;
        public Function func;
        public RadioButton rad;


        public bool NeedStop(RadioButton stop_style, double stop_param)
        {
            bool res = false;
            if (stop_style.Checked)
            {
               if (count_of_trials == stop_param)
                    res = true;
            }
            else
            {
                double x1 = serial_of_trials[num_of_max_interval_characteristics];
                double x0 = serial_of_trials[num_of_max_interval_characteristics - 1];
                if ((x1 - x0) < stop_param)
                    res = true;
            }
            return res;
        }

        public virtual void R_Calc()
        {
            list_of_interval_characteristics.Clear();
            double maxR = 0;
            for (int i = 1; i < count_of_trials; i++)
            {
                double R = serial_of_trials[i] - serial_of_trials[i - 1];
                list_of_interval_characteristics.Add(R);
                if (R > maxR)
                {
                    maxR = R;
                    num_of_max_interval_characteristics = i;
                }
            }
        }

        public virtual void NextTrialPoint()
        {
            double next_point = 0.5 * (serial_of_trials[num_of_max_interval_characteristics] + serial_of_trials[num_of_max_interval_characteristics - 1]);
            serial_of_trials.Add(next_point);
            count_of_trials++;
        }

        public void Sort()
        {
            serial_of_trials.Sort();
        }
        public virtual void Init(Function _func, double _a_border, double _b_border,RadioButton _rad, double _stop_param)
        {
            func = _func;
            a_border = _a_border;
            b_border = _b_border;
            serial_of_trials = new List<double>();
            list_of_interval_characteristics = new List<double>();
            num_of_max_interval_characteristics = 1;
            count_of_trials = 2;
            count_of_measurement_function = 0;
            rad = _rad;
            stop_param = _stop_param;
            list_of_interval_characteristics.Add(-1);
        }

        public void Start()
        {
            serial_of_trials.Add(a_border);
            serial_of_trials.Add(b_border);
            while(!NeedStop(rad,stop_param))
            {
                R_Calc(); // Ну не просто так же, мы вычисляем R.
                NextTrialPoint();
                Sort();
            }

        }

    }
}
