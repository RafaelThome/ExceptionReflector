using System;
using System.Configuration;
using System.Diagnostics;

namespace ExceptionReflector.Config
{
    [
        ConfigurationCollection
                                (
                                        itemType : typeof(ForbidenExceptionTypeConfigElement)
                                    ,
                                        AddItemName = "addForbidenExceptionType"
                                    , 
                                        ClearItemsName = "clearForbidenExceptionTypes"
                                    , 
                                        CollectionType =  ConfigurationElementCollectionType.AddRemoveClearMap
                                    , 
                                        RemoveItemName = "removeForbidenExceptionType"
                                )
    ]
    //[DebuggerStepThrough]
    public class ForbidenExceptionTypesConfigElemCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ForbidenExceptionTypeConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ForbidenExceptionTypeConfigElement)element).Value;
        }

        public ForbidenExceptionTypeConfigElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as ForbidenExceptionTypeConfigElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);

                this.BaseAdd(index, value);
            }
        }

        public new ForbidenExceptionTypeConfigElement this[string responseString]
        {
            get { return (ForbidenExceptionTypeConfigElement)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));

                BaseAdd(value);
            }
        }

        public int IndexOf(ForbidenExceptionTypeConfigElement ForbidenExceptionType)
        {
            return BaseIndexOf(ForbidenExceptionType);
        }

        public void Add(ForbidenExceptionTypeConfigElement ForbidenExceptionType)
        {
            BaseAdd(ForbidenExceptionType);
        }
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(ForbidenExceptionTypeConfigElement ForbidenExceptionType)
        {
            if (BaseIndexOf(ForbidenExceptionType) >= 0)
                BaseRemove(ForbidenExceptionType.Value);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(Type ForbidenExceptionType)
        {
            BaseRemove(ForbidenExceptionType);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
