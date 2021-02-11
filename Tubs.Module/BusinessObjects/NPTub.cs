using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
namespace Tubs.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [NavigationItem("Config")]
    public class NPTub : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged//: NonPersistentBaseObject ,IObjectSpaceLink
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<NPTub> GetData()
        {
            var l = new List<NPTub>();
            for (int i = 0; i < 50; i++)
            {
                var tub = new NPTub { Name = $"name {i}", Id = i };
                l.Add(tub);
            }

            return l.ToList();

        }

        public IObjectSpace ObjectSpace { get; set; }
        public void OnCreated()
        {
            //throw new System.NotImplementedException();
        }

        public void OnSaving()
        {
            //throw new System.NotImplementedException();
        }

        public void OnLoaded()
        {
            //throw new System.NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}