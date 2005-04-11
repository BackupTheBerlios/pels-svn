using System;
using System.Collections;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	 /// <summary>
	 /// Eine abstrakte Klasse, die das IDictionary Interface implemenitert und
	 /// remotable ist
	 /// </summary>
	 
     [Serializable]
     public abstract class Cdv_RemoteDictionaryBase : MarshalByRefObject, IDictionary{

		 #region Instanzvariablen
		 /// <summary>
		 /// Hashtable zur Verwaltung der Einträge
		 /// </summary>
         Hashtable hashtable;
		 #endregion
 
		 #region Methoden und Properties
         protected Hashtable InnerHashtable 
		 { 
             get { 
                 if (hashtable == null)
                     hashtable = new Hashtable();
                 return hashtable;
             }
         }
 
         protected IDictionary Dictionary {
             get { return (IDictionary) this; }
         }
 
         public int Count {
             get { return hashtable == null ? 0 : hashtable.Count; }
         }
 
         bool IDictionary.IsReadOnly {
             get { return InnerHashtable.IsReadOnly; }
         }
 
         bool IDictionary.IsFixedSize {
             get { return InnerHashtable.IsFixedSize; }
         }
 
         bool ICollection.IsSynchronized {
             get { return InnerHashtable.IsSynchronized; }
         }
 
         ICollection IDictionary.Keys {
             get {
                 return InnerHashtable.Keys;
             }
         }
 
         Object ICollection.SyncRoot {
             get { return InnerHashtable.SyncRoot; }
         }
 
         ICollection IDictionary.Values {
             get {
                 return InnerHashtable.Values;
             }
         }
 
         public void CopyTo(Array array, int index) {
             InnerHashtable.CopyTo(array, index);
         }
 
         object IDictionary.this[object key] {
             get {
                 OnGet(key, InnerHashtable[key]);
				 return InnerHashtable[key]; 
             }
             set { 
                 OnValidate(key, value);
                 Object temp = InnerHashtable[key];
                 OnSet(key, temp, value); 
                 InnerHashtable[key] = value; 
                 try {
                     OnSetComplete(key, temp, value); 
                 }
                 catch (Exception) {
                    InnerHashtable[key] = temp; 
                    throw;
                 }
             }
         }
 
         bool IDictionary.Contains(object key) {
             return InnerHashtable.Contains(key);
         }
 
         void IDictionary.Add(object key, object value) {
             OnValidate(key, value);
             OnInsert(key, value); 
             InnerHashtable.Add(key, value);
             try {
                 OnInsertComplete(key, value);
             }
             catch (Exception) {
                 InnerHashtable.Remove(key);
                 throw;
             }
         }
 
         public void Clear() {
             OnClear();
             InnerHashtable.Clear();
             OnClearComplete();
         }
 
         void IDictionary.Remove(object key) {
             Object temp = InnerHashtable[key];
             OnValidate(key, temp);
             OnRemove(key, temp);
             InnerHashtable.Remove(key);
             OnRemoveComplete(key, temp);
         }
 
         public IDictionaryEnumerator GetEnumerator() {
             return InnerHashtable.GetEnumerator();
         }
 
         IEnumerator IEnumerable.GetEnumerator() {
             return InnerHashtable.GetEnumerator();
         }
		 #endregion

		 #region Virtuelle Methoden
         protected virtual object OnGet(object key, object currentValue) 
		 {
             return currentValue;
         }
 
         protected virtual void OnSet(object key, object oldValue, object newValue) { 
         }
 
         protected virtual void OnInsert(object key, object value) { 
         }
 
         protected virtual void OnClear() { 
         }
 
         protected virtual void OnRemove(object key, object value) { 
         }
 
         protected virtual void OnValidate(object key, object value) {
         }
         
         protected virtual void OnSetComplete(object key, object oldValue, object newValue) { 
         }
 
         protected virtual void OnInsertComplete(object key, object value) { 
         }
 
         protected virtual void OnClearComplete() { 
         }
 
         protected virtual void OnRemoveComplete(object key, object value) { 
         }

		 public override object InitializeLifetimeService()
		 {
			 return(null);
		 }

		 #endregion
     }

}
