using WinForm;

namespace pN8_AsyncEvent
{
    public partial class Form1 : Form
    {
        private int iAttemptCounter;

        public Form1()
        {
            InitializeComponent();
            iAttemptCounter = 0;
        }

        private void butTaskWithoutWait_Click(object sender, EventArgs e)
        {
            EventPrintToGrid(false, 0, $".{iAttemptCounter,2:00} Attempt, Main procedure Start");

            StartPublisher(iAttemptCounter);

            EventPrintToGrid(false, 0, $".{iAttemptCounter,2:00} Attempt, Main procedure End");
            iAttemptCounter++;
        }

        //Create "publisher" instance class in new thread
        private void StartPublisher(int ciNum)
        {
            AsyncPublisher cPublisher;
            cPublisher = new AsyncPublisher(ciNum);

            //cPublisher.cDelegat += PrintToListBox;
            cPublisher.cDelegat3 += EventPrintToGrid; //Attaching a method to a publisher
            var atsk = cPublisher.GenerateAction();
            //The publisher instance will terminate when it finishes its thread's tasks.
        }

        //Event method (subscriber) compatibile with eventDelegatPrint3 signature
        private void EventPrintToGrid(bool isEvent, int num, string s)
        {
            int rowVisible = 24;

            Color _col = Color.DarkGreen;
            if (isEvent)
            {
                Color[] tcol = { Color.Red, Color.Purple, Color.Blue, Color.Brown };
                byte colBlue = (byte)((num % 4));
                _col = tcol[colBlue];
            }

            AddRow(s, _col);

            void AddRow(string text, Color color)
            {
                int rowIndex = dataGridView1.Rows.Add(text);
                dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = color;
                if (rowIndex > rowVisible)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }
        }
    }
}
