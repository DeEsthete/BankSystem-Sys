using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankSystem
{
    public class Operation
    {
        public delegate string OperationDelegate(CashMachine cm, Person p, int m);
        private OperationDelegate _delegateAddMoney;
        private OperationDelegate _delegateWirthdraw;

        public void RegisterHandler(OperationDelegate delAdd, OperationDelegate delTakeOff)
        {
            _delegateAddMoney = delAdd;
            _delegateWirthdraw = delTakeOff;
        }

        #region DelegateMethods
        public void AddMoney(CashMachine machine, Person person, int money)
        {
            IAsyncResult result = _delegateAddMoney.BeginInvoke(machine, person, money, new AsyncCallback(asyncResult => MessageBox.Show(_delegateAddMoney.EndInvoke(asyncResult))), null);
        }

        public void WithDraw(CashMachine machine, Person person, int money)
        {
            IAsyncResult result = _delegateWirthdraw.BeginInvoke(machine, person, money, new AsyncCallback(asyncResult => MessageBox.Show(_delegateWirthdraw.EndInvoke(asyncResult))), null);
        }
        #endregion
        
    }
}
