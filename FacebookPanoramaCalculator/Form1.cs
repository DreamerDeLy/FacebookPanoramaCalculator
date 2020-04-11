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
			long height = Int32.Parse(tb1.Text);
			long width = Int32.Parse(tb2.Text);

			long fv = Int32.Parse(tb3.Text);

			//MessageBox.Show($"{height} ; {width} ; {fv}");

			long width_full = (360 * width) / fv;
			long height_full = width_full / 2;

			long cp_left = (width_full - width) / 2;
			long cp_top = (height_full - height) / 2;

			string pattern = $"<x:xmpmeta xmlns:x =\"adobe:ns:meta/\" x:xmptk=\"Adobe XMP Core 5.3-c011 66.145661, 2012/02/06-14:56:27        \">\n<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n<rdf:Description rdf:about=\"\" xmlns:GPano=\"http://ns.google.com/photos/1.0/panorama/\">\n<GPano:CroppedAreaImageHeightPixels>{height}</GPano:CroppedAreaImageHeightPixels>\n<GPano:CroppedAreaImageWidthPixels>{width}</GPano:CroppedAreaImageWidthPixels>\n<GPano:FullPanoHeightPixels>{height_full}</GPano:FullPanoHeightPixels>\n<GPano:FullPanoWidthPixels>{width_full}</GPano:FullPanoWidthPixels>\n<GPano:CroppedAreaLeftPixels>{cp_left}</GPano:CroppedAreaLeftPixels>\n<GPano:CroppedAreaTopPixels>{cp_top}</GPano:CroppedAreaTopPixels>\n</rdf:Description>\n</rdf:RDF>\n</x:xmpmeta >";

			if (fv == 360) pattern = $"<x:xmpmeta xmlns:x =\"adobe:ns:meta/\" x:xmptk=\"Adobe XMP Core 5.3-c011 66.145661, 2012/02/06-14:56:27        \">\n<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n<rdf:Description rdf:about=\"\" xmlns:GPano=\"http://ns.google.com/photos/1.0/panorama/\">\n<GPano:CroppedAreaImageHeightPixels>{height}</GPano:CroppedAreaImageHeightPixels>\n<GPano:CroppedAreaImageWidthPixels>{width}</GPano:CroppedAreaImageWidthPixels>\n<GPano:FullPanoHeightPixels >{height_full}</GPano:FullPanoHeightPixels>\n<GPano:FullPanoWidthPixels>{width_full}</GPano:FullPanoWidthPixels>\n</rdf:Description>\n</rdf:RDF>\n</x:xmpmeta >";

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
			button1.PerformClick();

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
