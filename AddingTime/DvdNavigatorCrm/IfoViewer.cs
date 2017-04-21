using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DvdNavigatorCrm;

namespace DvdNavigatorCrm
{
	public partial class IfoViewer : Form
	{
		public IfoViewer()
		{
			InitializeComponent();
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			using(OpenFileDialog fd = new OpenFileDialog())
			{
				fd.CheckFileExists = true;
				fd.DefaultExt = "ifo";
				fd.Filter = "Ifo files (*.ifo)|*.ifo";
				if(fd.ShowDialog() == DialogResult.OK)
				{
					DvdTitleSet vts = new DvdTitleSet(fd.FileName);
					if(!vts.IsValidTitleSet)
					{
						this.ifoDumpEdit.Text = "Invalid File";
					}
					else
					{
						vts.Parse();
						this.ifoDumpEdit.Text = vts.ToString();
						this.ifoDumpEdit.Select(0, 0);
						this.ifoDumpEdit.ScrollToCaret();
					}
				}
			}
		}
	}
}