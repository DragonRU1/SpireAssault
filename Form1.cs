namespace SpireAssault
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Equipment eq = new(new int[] { 0, 1, 0, 0, 1, 1, 1 });
			Trimp t = new(eq);
			Enemy en = new(2, eq);
			Simulation s = new(t, en);
			s.ProcessFights(100000);
		}
	}
}