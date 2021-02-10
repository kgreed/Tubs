using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using Tubs.Module.BusinessObjects;
namespace Tubs.Module.Win.Controllers
{
    [System.ComponentModel.DesignerCategory("")]
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TubObjectViewController : ObjectViewController<ListView, NPTub>
    {
        public TubObjectViewController()
        {
            // InitializeComponent();
            TargetObjectType = typeof(NPTub);
            // Target required Views (via the TargetXXX properties) and create their Actions.
           
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting += os_ObjectsGetting;
            // Perform various tasks depending on the target View.
            ObjectSpace.Refresh();
            Frame.GetController<FilterController>()?.Active.SetItemValue("Workaround T890466", false);
            Frame.GetController<FilterController>()?.Active.RemoveItem("Workaround T890466");
        }

        private void os_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            var collection = new DynamicCollection((IObjectSpace) sender, e.ObjectType, e.Criteria, e.Sorting,
                e.InTransaction);
            collection.FetchObjects += DynamicCollection_FetchObjects;
            e.Objects = collection;
        }

        private void DynamicCollection_FetchObjects(object sender, FetchObjectsEventArgs e)
        {
            var o = new NPTub();
            e.Objects = o.GetData();
            e.ShapeData = true;
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
            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting -= os_ObjectsGetting;
        }
    }
}