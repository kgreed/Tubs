﻿using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
namespace Tubs.Module.BusinessObjects
{
    public abstract class NonPersistentObjectFactoryBase
    {
        public abstract object GetObjectByKey(Type objectType, object key);
        public abstract IEnumerable GetObjects(Type objectType, CriteriaOperator criteria, IList<DevExpress.Xpo.SortProperty> sorting);
        public virtual void SaveObjects(ICollection toInsert, ICollection toUpdate, ICollection toDelete) { }
    }
}