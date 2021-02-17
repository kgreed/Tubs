using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
namespace Tubs.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [NavigationItem("Config")]
    [ListViewFilter("Hide ticked before today", "true", "Hide Ticked Before Today", false, Index = 1)]
    [ListViewFilter("Include ticked before today", "true", "Include Ticked Before Today", false, Index = 2)]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class NonPersistentObject1 //: IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private string sampleProperty;

        public NonPersistentObject1()
        {
            Oid = Guid.NewGuid();
        }

        [Key]
        [Browsable(false)] // Hide the entity identifier from UI.
        public Guid Oid { get; set; }
        [XafDisplayName("My display name")]
        [ToolTip("My hint message")]
        [ModelDefault("EditMask", "(000)-00")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SampleProperty
        {
            get => sampleProperty;
            set
            {
                if (sampleProperty != value)
                {
                    sampleProperty = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region INotifyPropertyChanged members (see http: //msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https: //documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)

        //void IXafEntityObject.OnCreated()
        //{
        //    // Place the entity initialization code here.
        //    // You can initialize reference properties using Object Space methods; e.g.:
        //    // this.Address = objectSpace.CreateObject<Address>();
        //}
        //void IXafEntityObject.OnLoaded()
        //{
        //    // Place the code that is executed each time the entity is loaded here.
        //}
        //void IXafEntityObject.OnSaving()
        //{
        //    // Place the code that is executed each time the entity is saved here.
        //}

        #endregion

        #region IObjectSpaceLink members (see https: //documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)

        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        //IObjectSpace IObjectSpaceLink.ObjectSpace
        //{
        //    get { return objectSpace; }
        //    set { objectSpace = value; }
        //}

        #endregion
    }
}