using System;
using System.Diagnostics;
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

        public Form1()
        {
            InitializeComponent();
            Subscribe();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Icon = Properties.Resources.mic_off;
            this.BeginInvoke(new Action(() => {
                this.Hide();
                this.Opacity = 1;
            }));            
        }
        
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


        //Function

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

            //tbIcon.Text = result.DeviceFriendlyName;

            return result;
        }

        //HOTKEY
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt += GlobalHookMouseUpExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

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
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it 
            m_GlobalHook.Dispose();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void changeHotkeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
            this.Show();
        }

        private void changeHotkey_btn_Click(object sender, EventArgs e)
        {
            //hotKey_label.Text = "Waiting...";
            //setKeys = true;
            //_changeHotkey();

            //this.Enabled = false;
        }

    }
}
