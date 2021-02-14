using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DevExpress.ExpressApp.Data;
namespace Tubs.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [NavigationItem("Config")]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class NPObj : NonPersistentObjectBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }

//    [DefaultClassOptions]
//    [DefaultListViewOptions(true, NewItemRowPosition.Top)]
//    [DefaultProperty("PublicName")]
//    [DevExpress.ExpressApp.DC.DomainComponent]
//    public class Account : NonPersistentObjectBase
//    {
//        private string userName;
//        //[Browsable(false)]
//        [DevExpress.ExpressApp.ConditionalAppearance.Appearance("", Enabled = false, Criteria = "Not IsNewObject(This)")]
//        [RuleRequiredField]
//        [DevExpress.ExpressApp.Data.Key]
//        public string UserName
//        {
//            get { return userName; }
//            set { userName = value; }
//        }
//        public void SetKey(string userName)
//        {
//            this.userName = userName;
//        }
//        private string publicName;
//        public string PublicName
//        {
//            get { return publicName; }
//            set { SetPropertyValue(nameof(PublicName), ref publicName, value); }
//        }
//    }
 }