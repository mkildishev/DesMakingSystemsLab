using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesMakingSystemsLab
{
    class StronginMethod : ComputationalMethod
    {
        public override void M_and_m_Calc()
        {
            double max_M = 0;
            for (int i = 1; i < count_of_trials; i++)
            {
                double dz = base.GetFuncValue(serial_of_trials[i]) - base.GetFuncValue(serial_of_trials[i - 1]);
                double dx = serial_of_trials[i] - serial_of_trials[i - 1];
                double M = Math.Abs(dz / dx);
                if (M > max_M)
                    max_M = M;
            }
            if (max_M == 0)
                m = 1;
            else
                m = r * max_M;

        }

        public override void Start()
        {
            serial_of_trials.Add(a_border);
            serial_of_trials.Add(b_border);
            while (!NeedStop(rad, stop_param))
            {
                Sort();
                M_and_m_Calc();
                R_Calc();
                NextTrialPoint();
            }
        }

        public override void Init(Function _func, double _a_border, double _b_border,
            RadioButton _rad, double _stop_param, double _r)
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
            r = _r;
        }

        public override void NextTrialPoint()
        {
            double p1 = (serial_of_trials[num_of_max_interval_characteristics] + serial_of_trials[num_of_max_interval_characteristics - 1]);
            double p2 = base.GetFuncValue(serial_of_trials[num_of_max_interval_characteristics]) - base.GetFuncValue(serial_of_trials[num_of_max_interval_characteristics - 1]);
            double next_point = (0.5 * p1) - (p2 / (2 * m));
            serial_of_trials.Add(next_point);
            count_of_trials++;
        }

        public override void R_Calc()
        {
            list_of_interval_characteristics.Clear();
            double maxR = -9999999999;
            for (int i = 1; i < count_of_trials; i++)
            {
                double dz = (base.GetFuncValue(serial_of_trials[i]) + base.GetFuncValue(serial_of_trials[i - 1]));
                double dx = (serial_of_trials[i] - serial_of_trials[i - 1]);
                double R = 0.5 * m * dx - dz * 0.5;
                list_of_interval_characteristics.Add(R);
                if (R > maxR)
                {
                    maxR = R;
                    num_of_max_interval_characteristics = i;
                }
            }
        }
    }
}
