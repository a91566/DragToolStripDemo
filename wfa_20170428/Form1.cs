/*
 * 2017年4月28日22:11:17	郑少宝
 * 
 * 我喜欢夏天里阳光中的雨
 * 冬天里雾气中的阳光
 * 和任何时候的你
 */
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace wfa_20170428
{
	public partial class Form1 : Form
	{
		private ToolStrip _ts;
		public Form1()
		{
			InitializeComponent();
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.button1_Click(null,null);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_ts = new ToolStrip();
			_ts.Parent = this;
			_ts.Height = 58;
			_ts.AutoSize = false;
			_ts.ImageScalingSize = new Size(48,48);
			this.addButtons();
			button1.Enabled = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			openCloseDrag(true);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			openCloseDrag(false);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			foreach (ToolStripItem item in _ts.Items)
			{
				sb.Append($"{ item.ToolTipText}{ Environment.NewLine}");
			}
			MessageBox.Show(sb.ToString());
		}
		

		private void addButtons()
		{
			for (int i = 0; i < 10; i++)
			{
				ToolStripItem tsi;
				if (i % 2 == 0)
				{
					tsi = new ToolStripDropDownButton();
				}
				else
				{
					tsi = new ToolStripButton();
				}
				tsi.DisplayStyle = ToolStripItemDisplayStyle.Image;
				tsi.Image = this.getImage(i);
				tsi.ToolTipText = $"Number{ i }";
				tsi.Text = tsi.ToString();
				tsi.AllowDrop = true;
				tsi.MouseDown += (s, e) => this.DoDragDrop(s, DragDropEffects.Move);
				tsi.DragEnter += (s, e) => e.Effect = DragDropEffects.Move;
				tsi.DragDrop += (s, e) =>
				{
					ToolStripItem source = e.Data.GetData(typeof(ToolStripButton)) as ToolStripItem;
					//不知道为何不能用 e.Data.GetData(typeof(ToolStripItem)) 
					if (source == null)
					{
						source = e.Data.GetData(typeof(ToolStripDropDownButton)) as ToolStripItem;
					}
					//Point point = _ts.PointToClient(new Point(e.X, e.Y));
					//ToolStripItem target = _ts.GetItemAt(point);
					this.moveLocation(source, s as ToolStripItem);
				};
			
				_ts.Items.Add(tsi);
			}
		}

		private void moveLocation(ToolStripItem source, ToolStripItem target)
		{
			if (source == null || target == null) return;
			_ts.Items.Insert(_ts.Items.IndexOf(target), source);
		}

		private Image getImage(int i)
		{
			switch (i)
			{
				case 0:
					return Properties.Resources.Number0;
				case 1:
					return Properties.Resources.Number1;
				case 2:
					return Properties.Resources.Number2;
				case 3:
					return Properties.Resources.Number3;
				case 4:
					return Properties.Resources.Number4;
				case 5:
					return Properties.Resources.Number5;
				case 6:
					return Properties.Resources.Number6;
				case 7:
					return Properties.Resources.Number7;
				case 8:
					return Properties.Resources.Number8;
				case 9:
					return Properties.Resources.Number9;
			}
			return null;
		}

		private void openCloseDrag(bool isopen)
		{
			foreach (ToolStripItem tsb in this._ts.Items)
			{
				tsb.AllowDrop = isopen;
			}

		}
				
	}
}
