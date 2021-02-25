using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tubs.Module.BusinessObjects;
namespace Tubs.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TubsDetailViewController : ViewController
    {
        private RecordsNavigationController navigationController;
        public TubsDetailViewController()
        {
            
            InitializeComponent();
            TargetObjectType = typeof(NPTub);
            TargetViewType = ViewType.DetailView;
 
        }

        private void NextObjectAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Debug.Print("Next");
            var tub = View.CurrentObject as NPTub;
            tub.Phone.PhoneNumber = $"1000-{tub.Phone.PhoneNumber}"; // fires too late. The editor is already loaded
        }
        
        protected override void OnActivated()
        {
            base.OnActivated();
            navigationController = Frame.GetController<RecordsNavigationController>();
            navigationController.NextObjectAction.Execute += NextObjectAction_Execute; ;
            View.CurrentObjectChanged += View_CurrentObjectChanged;
            // Perform various tasks depending on the target View.
        }

        private void View_CurrentObjectChanged(object sender, EventArgs e)
        {
            var v = sender as DetailView;
            var tub = v.CurrentObject as NPTub;
            tub.Phone.PhoneNumber = $"300-{tub.Phone.PhoneNumber}";
        }

        protected override void OnViewChanged()
        {
            base.OnViewChanged();
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            View.CurrentObjectChanged -= View_CurrentObjectChanged;
        }
    }
}
