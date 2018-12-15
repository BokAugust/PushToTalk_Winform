using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using NAudio.CoreAudioApi;

namespace PushToTalk_Winform
{
    public partial class Form1 : Form
    {

        private IKeyboardMouseEvents m_GlobalHook;
        public MouseButtons unmuteDefault = MouseButtons.XButton1;
        public MouseButtons toggleDefault = MouseButtons.XButton2;
        public System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();
            Subscribe();
            setTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            muteMic();

            notifyIcon1.Icon = Properties.Resources.mic_off;
            BeginInvoke(new Action(() => {
                this.Hide();
                this.Opacity = 1;
            }));

            //check the checkbox of memory
            if (this.autoOptimizeMemoryToolStripMenuItem.Checked == true)
            {
                timer.Enabled = true;
                timer.Start();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        #region Fuction About Control Mics
        public void muteMic()
        {
            this.notifyIcon1.Icon = Properties.Resources.mic_off;
            setMicMuteStatus(true);
        }

        public void unmuteMic()
        {
            this.notifyIcon1.Icon = Properties.Resources.mic_on;
            setMicMuteStatus(false);
        }

        public void setMicMuteStatus(bool doMute)
        {
            var device = getPrimaryMicDevice();

            if (device != null)
            {
                device.AudioEndpointVolume.Mute = doMute;
                updateMicStatus(device);
            }
            else
            {
                updateMicStatus(null);
            }
        }

        public void changeMicVolume(double number)
        {
            var device = getPrimaryMicDevice();
            var volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;

            if (device != null)
            {
                device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)number;
                Trace.WriteLine(device.AudioEndpointVolume.MasterVolumeLevelScalar);
                updateMicStatus(device);
            }
            else
            {
                updateMicStatus(null);
            }
        }

        private void updateMicStatus(MMDevice device)
        {
            disposeDevice(device);
        }

        private void disposeDevice(MMDevice device)
        {
            if (device != null)
            {
                device.AudioEndpointVolume.Dispose();
                device.Dispose();
            }
        }
        private MMDevice getPrimaryMicDevice()
        {
            var enumerator = new MMDeviceEnumerator();
            var result = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
            enumerator.Dispose();
            return result;
        }
        #endregion

        #region Function About Hotkeys
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt += GlobalHookMouseUpExt;
            //m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        //private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        //}

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            if (e.Button == toggleDefault)
            {
                unmuteMic();
                e.Handled = true;
                hotKey_label.Text = toggleDefault.ToString();
            }

            if (e.Button == unmuteDefault)
            {
                unmuteMic();
                e.Handled = true;
            }
        }

        private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            if (e.Button == toggleDefault)
            {
                muteMic();
                e.Handled = true;
                hotKey_label.Text = toggleDefault.ToString();
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            //m_GlobalHook.KeyPress -= GlobalHookKeyPress;
            //It is recommened to dispose it 
            m_GlobalHook.Dispose();
        }
        #endregion

        #region Function About Memorys
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        public static void ClearMemory()
        {

            //1st
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //2st
            Process.GetCurrentProcess().MinWorkingSet = new IntPtr(5);

            //3st
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }

            //4st
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if ((process.ProcessName == "System") && (process.ProcessName == "Idle"))
                    continue;
                try
                {
                    EmptyWorkingSet(process.Handle);
                }
                catch (Exception e)
                {
                    //Some process might be denied.
                    //Trace.WriteLine(e.ToString());
                }
            }
        }

        public void setTimer()
        {
            // Create a timer with a 1 minute interval.
            timer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            ClearMemory();
            Trace.WriteLine("memory...");
        }

        private void autoOptimizeMemoryToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (autoOptimizeMemoryToolStripMenuItem.Checked == false)
            {
                timer.Enabled = false;
                timer.Stop();
                Trace.WriteLine("you were turn off the memory");
            }
            else
            {
                timer.Enabled = true;
                timer.Start();
                Trace.WriteLine("you were turn on the memory");
            }
        }

        private void autoOptimizeMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (autoOptimizeMemoryToolStripMenuItem.Checked == true)
                autoOptimizeMemoryToolStripMenuItem.Checked = false;
            else
                autoOptimizeMemoryToolStripMenuItem.Checked = true;
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_GlobalHook.Dispose();
            Application.Exit();
        }

        private void changeHotkeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
            this.Show();
        }

        private void changeHotkey_btn_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        #region The event of Volume

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.1);
            toolStripMenuItem1.Checked = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.2);
            toolStripMenuItem2.Checked = true;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.3);
            toolStripMenuItem3.Checked = true;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.4);
            toolStripMenuItem4.Checked = true;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.5);
            toolStripMenuItem5.Checked = true;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.6);
            toolStripMenuItem6.Checked = true;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.7);
            toolStripMenuItem7.Checked = true;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.8);
            toolStripMenuItem8.Checked = true;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(0.9);
            toolStripMenuItem9.Checked = true;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            cleanChecked();
            changeMicVolume(1);
            toolStripMenuItem10.Checked = true;
        }

        private void cleanChecked()
        {
            toolStripMenuItem1.Checked = false;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = false;
        }
    

        #endregion

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            var device = getPrimaryMicDevice();
            var volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            string volume1 = string.Format("{0:0%}", volume);
            changeVolumeToolStripMenuItem.Text = "Change Mic Volume" + " ( " + volume1 + " )";
        }
    }
}
