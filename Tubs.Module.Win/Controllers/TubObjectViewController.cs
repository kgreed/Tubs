using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using Tubs.Module.BusinessObjects;
namespace Tubs.Module.Win.Controllers
{
    [System.ComponentModel.DesignerCategory("")]
      public partial class TubObjectViewController : ObjectViewController<ListView, NPTub>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting += os_ObjectsGetting;
            // Perform various tasks depending on the target View.
            ObjectSpace.Refresh();
            Frame.GetController<FilterController>()?.Active.SetItemValue("Workaround T890466", false);
            Frame.GetController<FilterController>()?.Active.RemoveItem("Workaround T890466");
            View.SelectionChanged += View_SelectionChanged;
        }

        private void View_SelectionChanged(object sender, System.EventArgs e)
        {
            var lv = sender as ListView;
            var tub = lv.CurrentObject as NPTub;
            tub.Phone.PhoneNumber = $"000-{tub.Phone.PhoneNumber}";

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

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting -= os_ObjectsGetting;
        }
    }
}