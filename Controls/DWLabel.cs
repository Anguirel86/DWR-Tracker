using DWR_Tracker.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DWR_Tracker.Controls
{
    public partial class DWLabel : Label
    {
        public DWLabel()
        {
            InitializeComponent();
            Font = new Font(DWGlobals.DWFont.GetFamily(), 12);
            ForeColor = Color.FromArgb(255, 255, 255);
        }

        public void FitText(string text, float minRatio)
        {
            FitText(text, false, FontStyle.Regular, minRatio);
        }

        public void FitText(string text, bool fitToParent = false, FontStyle style = FontStyle.Regular, float minRatio = 1f)
        {
            if (text == null || text.Length == 0) { return; }
            if (fitToParent)
            {
                this.Height = this.Parent.Size.Height;
                this.Width = this.Parent.Size.Width;
            }
            float width = this.Width * 0.95f;

            this.SuspendLayout();

            Font tryFont = new Font(DWGlobals.DWFont.GetFamily(), 12, style);
            Size trySize = TextRenderer.MeasureText(text, tryFont);
            float widthRatio = width / trySize.Width;
            tryFont = new Font(DWGlobals.DWFont.GetFamily(), 
                tryFont.Size * Math.Min(widthRatio, minRatio), style);
            this.Font = tryFont;
            this.Text = text;

            this.ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
