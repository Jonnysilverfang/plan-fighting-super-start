using System;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace plan_fighting_super_start 
{
    public partial class GAMEBOSS : Form
    {
        private WindowsMediaPlayer _player;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var mp3Path = Path.Combine(Application.StartupPath, "bossgame.mp3"); // Copy to Output
            _player = new WindowsMediaPlayer();
            _player.URL = mp3Path;
            _player.settings.setMode("loop", true);
            _player.settings.volume = 40; // 0..100
            _player.controls.play();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try { _player?.controls.stop(); _player?.close(); } catch { }
            base.OnFormClosed(e);
        }
    }
}
