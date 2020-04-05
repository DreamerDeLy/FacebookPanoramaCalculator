using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;            // File
using System.Text.RegularExpressions; // Regex

namespace FacebookPanoramaCalculator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int height = Int32.Parse(tb1.Text);
			int width = Int32.Parse(tb1.Text);

			int fv = Int32.Parse(tb3.Text);

			int width_full = (360 * width) / fv;
			int height_full = width_full / 2;

			int cp_left = (width_full - width) / 2;
			int cp_top = (height_full - height) / 2;

			string pattern = $"<GPano:CroppedAreaImageHeightPixels>{height}</GPano:CroppedAreaImageHeightPixels>\n< GPano:CroppedAreaImageWidthPixels >{width}</ GPano:CroppedAreaImageWidthPixels >\n< GPano:FullPanoHeightPixels >{height_full}</ GPano:FullPanoHeightPixels >\n< GPano:FullPanoWidthPixels >{width_full}</ GPano:FullPanoWidthPixels >\n< GPano:CroppedAreaLeftPixels >{cp_left}</ GPano:CroppedAreaLeftPixels >\n< GPano:CroppedAreaTopPixels >{cp_top}</ GPano:CroppedAreaTopPixels >";

			richTextBox1.Text = pattern;
		}

		private void tb1_KeyPress(object sender, KeyPressEventArgs e)
		{
			// ввод в texBox только цифр и кнопки Backspace
			char ch = e.KeyChar;
			if (!Char.IsDigit(ch) && ch != 8)
			{
				e.Handled = true;
			}
		}

		private void tb2_KeyPress(object sender, KeyPressEventArgs e)
		{
			// ввод в texBox только цифр и кнопки Backspace
			char ch = e.KeyChar;
			if (!Char.IsDigit(ch) && ch != 8)
			{
				e.Handled = true;
			}
		}

		private void tb3_KeyPress(object sender, KeyPressEventArgs e)
		{
			// ввод в texBox только цифр и кнопки Backspace
			char ch = e.KeyChar;
			if (!Char.IsDigit(ch) && ch != 8)
			{
				e.Handled = true;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string[] result_strings = new[] { richTextBox1.Text };

			Regex regEx = new Regex("[A-Za-z0-9_-]*[.]*[A-Za-z0-9]{3,4}");

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					string path = saveFileDialog1.FileName;

					if (!regEx.Match(path).Success)
					{
						path += ".xmp";
					}

					File.WriteAllLines(path, result_strings);
				}
				catch
				{
					MessageBox.Show("Error while saving file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
