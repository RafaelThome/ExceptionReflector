using System;
using System.Configuration;
using System.Diagnostics;

namespace ExceptionReflector.Config
{
    //[DebuggerStepThrough]
    public class ExceptionReflectorConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("BypassOpCodeExceptions", DefaultValue = "false", IsRequired = false, IsKey = false)]
        public Boolean BypassOpCodeExceptions
        {
            get
            {
                return (Boolean)this["BypassOpCodeExceptions"];
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ForbidenExceptionTypesConfigElemCollection ForbidenExceptionTypes
        {
            get
            {
                return (ForbidenExceptionTypesConfigElemCollection)this[""];
            }
        }
    }
}
