using System;
using System.Threading.Tasks;
using System.Threading;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//==== Author: janluksoft@interia.pl ==========================

namespace WinForm
{
    public class AsyncPublisher
    {
        private int ciNum;

        //The event should have such a signature somewhere
        public delegate void eventDelegatPrint(string s); //Delegate signature for the event
        public delegate void eventDelegatPrint3(bool isEvent, int num, string s); //Delegate signature

            //The [cDelegate3] field stores the 'delegate', i.e. a reference/pointer to the 'remote' function
            //run in another window.AsyncPublisher doesn't even know where this function is, it only has the 'link'
        public event eventDelegatPrint  cDelegat; //'cDelegat'    reference to the 'remote' event function
        public event eventDelegatPrint3 cDelegat3; //'cDelegat'   reference to the 'remote' event function
            // The above code defines a delegate called PrintToTextBox3 and an event called
            // called cDelegate3, which will invoke the delegate when the event is raised (called).

        public AsyncPublisher(int ciNum)
        {
            this.ciNum = ciNum;
        }

        public async Task GenerateAction()
        {
            int nmax = GenerateNum(2, 8);
            string mess = $" {ciNum,2:00} Attempt, 0/{nmax} external Event (start)";

            cDelegatSafety(true, ciNum, mess);

            for (int i = 1; i < (nmax+1); i++)
            {
                int tms = GenerateNum(200, 1200); //Generate random pause [ms]
                await Task.Delay(tms);
                mess = $" {ciNum,2:00} Attempt, {i}/{nmax} external Event ({tms} ms)";
                cDelegatSafety(true, ciNum, mess);
            }
        }

        private void cDelegatSafety(string s)
        {
            if (cDelegat != null)
                cDelegat(s);
        }

        //Event method cDelegatSafety compatibile with 
        private void cDelegatSafety(bool isEvent, int num, string s)
        {
            if (cDelegat3 != null) //checks if event methods are attached to the delegate field
                cDelegat3(isEvent, num, s); //executes event methods
        }

        private int GenerateNum(int xfrom, int xto)
        {
            Random rnd = new Random();
            int month = rnd.Next(xfrom, xto);  // creates a number between 1 and 12
            return (month);
        }

    }

}
