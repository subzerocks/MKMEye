using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectX.Capture;
using DShowNET;
using DShowNET.Device;

namespace MKMEye
{
    public partial class CamSelect : Form
    {

        private readonly Filters cameraFilters = new Filters();

        public CamSelect()
        {
            InitializeComponent();

            for (int i = 0; i != cameraFilters.VideoInputDevices.Count; i++)
            {

                camsAvailable.Items.Add(cameraFilters.VideoInputDevices[i].Name);

                camsAvailable.SelectedItem = cameraFilters.VideoInputDevices[i].Name;

                selectCamButton.Enabled = true;
            }


        }

        private void selectCamButton_Click(object sender, EventArgs e)
        {
            MainView mainView = new MainView();

            mainView.selectedCamIndex = camsAvailable.SelectedIndex;

            this.Hide();

            mainView.ShowDialog();

    
        }
    }
}
