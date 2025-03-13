using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace JQControls
{
    public class SimpleItem { public string Value { set; get; } }

    public class SimpleItemCollection : StateManagedCollection
    {
        [Browsable(false)]
        public SimpleItem this[int index] { get { return ((IList)this)[index] as SimpleItem; } }

        private static readonly Type[] knownTypes = new Type[] { typeof(SimpleItem) };
        protected override Type[] GetKnownTypes() { return knownTypes; }

        public void Add(SimpleItem item) { ((IList)this).Add(item); }

        public bool Contains(SimpleItem item) { return ((IList)this).Contains(item); }

        protected override object CreateKnownType(int index) { return new SimpleItem(); }

        public int IndexOf(SimpleItem item) { return ((IList)this).IndexOf(item); }

        public void Insert(int index, SimpleItem item) { ((IList)this).Insert(index, item); }

        public void Remove(SimpleItem item) { ((IList)this).Remove(item); }

        public void RemoveAt(int index) { ((IList)this).RemoveAt(index); }

        protected override void SetDirtyObject(object o) { }
    }
}