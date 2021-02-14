//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using DevExpress.ExpressApp;
//using DevExpress.Persistent.Base;
//using Tubs.Module.BusinessObjects;
//// ...
//public sealed partial class MyModule : ModuleBase
//{
//    private static Dictionary<int, NPObj> ObjectsCache
//    {
//        get
//        {
//            var manager = ValueManager.GetValueManager<Dictionary<int, NPObj>>("NP");
//            Dictionary<int, NPObj> objectsCache = (manager.CanManageValue) ? manager.Value : null;
//            if (objectsCache == null)
//            {
//                objectsCache = new Dictionary<int, NPObj>
//                {
//                    {0, new NPObj(0, "A")},
//                    {1, new NPObj(1, "B")},
//                    {2, new NPObj(2, "C")},
//                    {3, new NPObj(3, "D")},
//                    {4, new NPObj(4, "E")}
//                };
//                if (manager.CanManageValue)
//                {
//                    manager.Value = objectsCache;
//                }
//            }
//            return objectsCache;
//        }
//    }
//    //...
//    public override void Setup(XafApplication application)
//    {
//        base.Setup(application);
//        application.SetupComplete += Application_SetupComplete;
//    }
//    private void Application_SetupComplete(object sender, EventArgs e)
//    {
//        Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
//    }
//    private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e)
//    {
//        NonPersistentObjectSpace nonPersistentObjectSpace = e.ObjectSpace as NonPersistentObjectSpace;
//        if (nonPersistentObjectSpace != null)
//        {
//            nonPersistentObjectSpace.ObjectsGetting += NonPersistentObjectSpace_ObjectsGetting;
//            nonPersistentObjectSpace.ObjectByKeyGetting += NonPersistentObjectSpace_ObjectByKeyGetting;
//            nonPersistentObjectSpace.ObjectGetting += NonPersistentObjectSpace_ObjectGetting;
//            nonPersistentObjectSpace.Committing += NonPersistentObjectSpace_Committing;
//        }
//    }
//    private void NonPersistentObjectSpace_ObjectsGetting(Object sender, ObjectsGettingEventArgs e)
//    {
//        if (e.ObjectType == typeof(NPObj))
//        {
//            IObjectSpace objectSpace = (IObjectSpace)sender;
//            BindingList<NPObj> objects = new BindingList<NPObj> {AllowNew = true, AllowEdit = true, AllowRemove = true};
            
           

//            foreach (NPObj obj in ObjectsCache.Values)
//            {
//                objects.Add(objectSpace.GetObject<NPObj>(obj));
//            }
//            e.Objects = objects;
//        }
//    }
//    private void NonPersistentObjectSpace_ObjectByKeyGetting(object sender, ObjectByKeyGettingEventArgs e)
//    {
//        IObjectSpace objectSpace = (IObjectSpace)sender;
//        if (e.ObjectType == typeof(NPObj))
//        {
//            NPObj obj;
//            if (ObjectsCache.TryGetValue((int)e.Key, out obj))
//            {
//                e.Object = objectSpace.GetObject(obj);
//            }
//        }
//    }
//    private void NonPersistentObjectSpace_ObjectGetting(object sender, ObjectGettingEventArgs e)
//    {
//        if (e.SourceObject is NPObj)
//        {
//            ((IObjectSpaceLink)e.TargetObject).ObjectSpace = (IObjectSpace)sender;
//        }
//    }
//    private void NonPersistentObjectSpace_Committing(Object sender, CancelEventArgs e)
//    {
//        IObjectSpace objectSpace = (IObjectSpace)sender;
//        foreach (Object obj in objectSpace.ModifiedObjects)
//        {
//            NPObj myobj = obj as NPObj;
//            if (obj != null)
//            {
//                if (objectSpace.IsNewObject(obj))
//                {
//                    int key = ObjectsCache.Count;
//                    myobj.ID = key;
//                    ObjectsCache.Add(key, myobj);
//                }
//                else if (objectSpace.IsDeletedObject(obj))
//                {
//                    ObjectsCache.Remove(myobj.ID);
//                }
//            }
//        }
//    }
//}