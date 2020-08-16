using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace HardwareMonitor
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        private Computer computer = new Computer();

        public Form1()
        {
            InitializeComponent();

            computer.CPUEnabled = true;
            computer.GPUEnabled = true;
            computer.RAMEnabled = true;
            computer.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroProgressSpinner1.Visible)
                metroProgressSpinner1.Visible = false;
            else
                metroProgressSpinner1.Visible = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(500);
                foreach (IHardware hardvare in computer.Hardware)
                {
                    hardvare.Update();
                    switch (hardvare.HardwareType)
                    {
                        case HardwareType.CPU:
                            foreach (ISensor sensor in hardvare.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Clock)
                                {
                                    Action setText = new Action(() => { materialLabel1.Text = $"{sensor.Name}: {sensor.Value}"; });
                                    materialLabel1.Invoke(setText);
                                }
                                if (sensor.SensorType == SensorType.Temperature)
                                {
                                    Action setText = new Action(() => { materialLabel2.Text = $"{sensor.Name}: {sensor.Value}"; });
                                    materialLabel2.Invoke(setText);
                                }
                            }
                            break;
                        case HardwareType.GpuNvidia:
                            foreach (ISensor sensor in hardvare.Sensors)
                            {
                                if (sensor.SensorType == SensorType.SmallData)
                                {
                                    Action setText = new Action(() => { materialLabel3.Text = $"{sensor.Name}: {sensor.Value}"; });
                                    materialLabel3.Invoke(setText);
                                }
                                if (sensor.SensorType == SensorType.Temperature)
                                {
                                    Action setText = new Action(() => { materialLabel4.Text = $"{sensor.Name}: {sensor.Value}"; });
                                    materialLabel4.Invoke(setText);
                                }

                            }
                            break;
                        case HardwareType.RAM:
                            foreach (ISensor sensor in hardvare.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Data)
                                {
                                    Action setText = new Action(() => { materialLabel5.Text = $"{sensor.Name}: {sensor.Value}"; }) ;
                                    materialLabel5.Invoke(setText);
                                }
                                if (sensor.SensorType == SensorType.Load)
                                {
                                    Action setText = new Action(() => { materialLabel6.Text = $"{sensor.Name}: {sensor.Value}"; });
                                    materialLabel6.Invoke(setText);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
