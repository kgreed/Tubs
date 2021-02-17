using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Repository;
using Tubs.Module.BusinessObjects;
namespace Tubs.Module.Win.Editors
{
    [PropertyEditor(typeof(PhoneNo), true)]
    public class PhoneEditor :WinPropertyEditor
    {
        public PhoneEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }
        private TextBox control = null;
        protected override void ReadValueCore()
        {
            if (control == null) return;
            if (CurrentObject != null)
            {
                control.ReadOnly = false;
                var ph = PropertyValue as PhoneNo;
                control.Text= ph.PhoneNumber;
            }
            else
            {
                control.ReadOnly = true;
                control.Text = "";
            }
        }

        protected override void WriteValueCore()
        {
            base.WriteValueCore();
            var ph = PropertyValue as PhoneNo;
            ph.PhoneNumber = control.Text;
        }

        private void control_ValueChanged(object sender, EventArgs e)
        {
            if (IsValueReading) return;
            OnControlValueChanged();
            WriteValueCore();
        }
        protected override object CreateControlCore()
        {
            control = new TextBox();
            control.TextChanged += control_ValueChanged;
            return control;
        }
        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            ReadValue();
        }
       
        protected override void Dispose(bool disposing)
        {
            if (control != null)
            {
                control.TextChanged -= control_ValueChanged;
                control = null;
            }
            base.Dispose(disposing);
        }
        //RepositoryItem IInplaceEditSupport.CreateRepositoryItem()
        //{
        //    RepositoryItemSpinEdit item = new RepositoryItemSpinEdit();
        //    item.MinValue = 0;
        //    item.MaxValue = 5;
        //    item.Mask.EditMask = "0";
        //    return item;
        //}
        protected override object GetControlValueCore()
        {
            return (string) control?.Text;
        }
    }
}
