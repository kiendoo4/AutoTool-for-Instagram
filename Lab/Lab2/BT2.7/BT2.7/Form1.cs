using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace BT2._7
{
    public partial class frmPower : Form
    {
        private Process process = new Process();
        private ProcessStartInfo startInfo = new ProcessStartInfo();
        private int remain_seconds = 0;
        private string temp = "";
        public frmPower()
        {
            InitializeComponent();
        }
        private void frmPower_Load(object sender, EventArgs e)
        {
            radShutDown.Checked = true;
        }
        private void cdTimer_Tick(object sender, EventArgs e)
        {
            remain_seconds--;
            lblTime.Text = remain_seconds.ToString();
            if (remain_seconds == 0)
            {
                cdTimer.Stop();
                if (radLogOut.Checked) LogOut(sender, e);
                else if (radRestart.Checked) Restart(sender, e);
                else if (radShutDown.Checked) ShutDown(sender, e);
            }
        }
        private void ShutDown(object sender, EventArgs e)
        {
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "/s /t 0";
            process.StartInfo = startInfo;
            process.Start();
            process.Close();
        }
        private void Restart(object sender, EventArgs e)
        {
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "/r /t 0";
            process.StartInfo = startInfo;
            process.Start();
            process.Close();
        }
        private void LogOut(object sender, EventArgs e)
        {
            startInfo.FileName = "shutdown";
            startInfo.Arguments = "/l";
            process.StartInfo = startInfo;
            process.Start();
            process.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cdTimer.Stop();
            lblTime.Text = temp;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            temp = tbTime.Text;
            lblTime.Text = tbTime.Text;
            remain_seconds = Convert.ToInt32(lblTime.Text);
            cdTimer.Start();
        }
    }
}